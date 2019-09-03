using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.ContactInfo;
using HMS.Model.Core.DTOs.Customer;
using HMS.Web.Controllers.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Telerik.Windows.Documents.Flow.Model.Lists;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }

        #region Customer

        public JsonResult Customer_Read(string search)
        {
            var customers = uow.Customer.GetAll().ToList();

            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    FirstName = customer.FirstName ?? "",
                    LastName = customer.LastName ?? "",
                    NationalNo = customer.NationalNo ?? "",
                    PassportNo = customer.PassportNo ?? "",
                    Id = customer.Id
                });
            }

            if (search != null)
            {
                return Json(customerDtos.Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.NationalNo.Contains(search) || c.PassportNo.Contains(search))
                    .OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList(), JsonRequestBehavior.AllowGet);
            }

            return Json(customerDtos.OrderBy(c => c.LastName).ThenBy(c => c.FirstName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Customer_Delete(Guid? id)
        {
            if (id == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var contact = uow.ContactInfo.Get(c => c.Person.Id == id).SingleOrDefault();
            if (contact != null)
            {
                uow.ContactInfo.Remove(contact);
                uow.Complete();
            }

            var user = uow.User.Get(c => c.Person.Id == id).SingleOrDefault();
            if (user != null)
            {
                uow.User.Remove(user);
                uow.Complete();
            }

            var customer = uow.Customer.Find(id);
            if (customer != null)
            {
                uow.Customer.Remove(customer);
                uow.Complete();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Create Customer and a user for it
        /// </summary>
        /// <param name="customerDto">Customer and user details</param>
        /// <param name="parentId">The Id of the parent customer</param>
        /// <returns></returns>
        public ActionResult Customer_Create(CreateCustomerDto customerDto, string parentId)
        {
            if (ModelState.IsValid)
            {
                Guid customerParentId = new Guid();

                if (parentId != null)
                {
                    customerParentId = Guid.Parse(parentId);
                }

                var customer = new Model.Core.DomainModels.Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    NationalNo = customerDto.NationalNo,
                    PassportNo = customerDto.PassportNo,
                    ParentId = customerParentId
                };
                uow.Customer.Add(customer);

                PasswordHasher hasher = new PasswordHasher();
                Random random = new Random();

                // Create new user 
                ApplicationUser user = new ApplicationUser()
                {
                    Email = customerDto.Email,
                    UserName = customerDto.Email,
                    PasswordHash = hasher.HashPassword(customerDto.Password),
                    Person = customer,
                    SecurityStamp = random.Next(100000).ToString()
                };
                uow.User.Add(user);
                uow.Complete();

                // Create new role 
                if (!roleManager.RoleExists("User"))
                {
                    roleManager.Create(new IdentityRole()
                    {
                        Name = "User"
                    });
                }

                // Set Role 
                userManager.AddToRole(user.Id, "User");

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FelowCustomer_Read(string Id)
        {
            var customerParentId = Guid.Parse(Id);

            var felowCustomers = uow.Customer.Get(c => c.ParentId == customerParentId).ToList();
            List<GetCustomerDto> dtos = new List<GetCustomerDto>();


            foreach (var felow in felowCustomers)
            {
                dtos.Add(new GetCustomerDto()
                {
                    Id = felow.Id,
                    FirstName = felow.FirstName,
                    LastName = felow.LastName,
                    PassportNo = felow.PassportNo,
                    NationalNo = felow.NationalNo
                });
            }

            return Json(dtos, JsonRequestBehavior.AllowGet);
        }


        #endregion



        #region Contact

        /// <summary>
        /// Read the contacts of a special user from the database 
        /// </summary>
        /// <param name="Id">Customer Id</param>
        public ActionResult Contact_Read(Guid Id)
        {
            var contacts = uow.ContactInfo.Get(c => c.Person.Id == Id).ToList();
            var contactInfos = new List<ContactInfoDto>();

            string telType;

            foreach (var contact in contacts)
            {
                switch (contact.TelType)
                {
                    case TelType.Home:
                        telType = "خانه";
                        break;
                    case TelType.Work:
                        telType = "محل کار";
                        break;
                    case TelType.Mobile:
                        telType = "موبایل";
                        break;
                    default:
                        telType = "موبایل";
                        break;
                }

                contactInfos.Add(new ContactInfoDto()
                {
                    Id = contact.Id,
                    TelNo = contact.TelNo,
                    TelType = telType,
                    Address = contact.Address
                });
            }

            return Json(contactInfos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact_Delete(Guid Id)
        {
            var contact = uow.ContactInfo.Find(Id);
            uow.ContactInfo.Remove(contact);
            uow.Complete();

            return Json(true);
        }

        /// <summary>
        /// Create a new Contact info for a specific customer 
        /// </summary>
        public ActionResult Contact_Create(ContactInfoDto model)
        {
            if (ModelState.IsValid)
            {
                var customer = uow.Customer.Find(model.Id);

                TelType teltype = new TelType();
                switch (model.TelType)
                {
                    case "0":
                        teltype = TelType.Mobile;
                        break;
                    case "1":
                        teltype = TelType.Home;
                        break;
                    case "2":
                        teltype = TelType.Work;
                        break;
                }

                var contactInfo = new ContactInfo()
                {
                    TelType = teltype,
                    TelNo = model.TelNo,
                    Address = model.Address,
                    Person = customer
                };

                uow.ContactInfo.Add(contactInfo);
                uow.Complete();


                customer.Infos.Add(contactInfo);
                uow.Customer.Update(customer);
                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Partial Views


        public PartialViewResult _CreateCustomer()
        {
            return PartialView();
        }

        public PartialViewResult _CustomerDetails(Guid Id)
        {
            var customer = uow.Customer.Find(Id);
            var customerDto = new GetCustomerDto()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalNo = customer.NationalNo,
                PassportNo = customer.PassportNo
            };

            return PartialView(customerDto);
        }

        public PartialViewResult _CreateContact(Guid Id)
        {
            return PartialView();
        }

        public PartialViewResult _CreateFelowCustomer(Guid Id)
        {
            // Gets the felow of the selected customer
            var customerFelows = uow.Customer.Get(c => c.ParentId == Id).ToList();

            CustomerFelowDto dto = new CustomerFelowDto()
            {
                ParentCustomerId = Id,
                FelowCustomer = customerFelows
            };

            return PartialView(dto);
        }


        #endregion
    }
}