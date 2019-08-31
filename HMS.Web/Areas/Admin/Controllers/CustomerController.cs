using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Customer;
using HMS.Web.Controllers.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Customer_Read(string search)
        {
            var customers = uow.Customer.GetAll().ToList();

            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
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
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            var customer = uow.Customer.Find(id);
            if (customer != null)
            {
                uow.Customer.Remove(customer);
                uow.Complete();
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create Customer and a user for it
        /// </summary>
        public ActionResult Customer_Create(CreateCustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = new Model.Core.DomainModels.Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    NationalNo = customerDto.NationalNo,
                    PassportNo = customerDto.PassportNo
                };
                uow.Customer.Add(customer);
                uow.Complete();

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

        public ActionResult Contact_Create()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}