using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Customer;
using HMS.Model.Core.ViewModels;
using HMS.Service.Core;
using HMS.Service.Persistance;
using HMS.Web.Models;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class ManageCustomerController : Controller
    {
        #region Propeties and constructors 

        private readonly IUnitOfWork uow;

        public ManageCustomerController()
        {
            // ============================================================================
            // REMEMBER TO ADD DEPENDENCY INJECTION LATER WHEN THE ACCESS TO NUGET IS OPEN 
            // ============================================================================

            uow = new UnitOfWork(new ApplicationDbContext());
        }

        #endregion


        // GET: Admin/ManageCustomer
        public ActionResult Index()
        {
            return View(new TelType());
        }

        public JsonResult Read()
        {
            var customers = uow.Customer.GetAll();
            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                GetCustomerDto customerDto = new GetCustomerDto()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    NationalNo = customer.NationalNo,
                    PassportNo = customer.PassportNo
                };
                customerDtos.Add(customerDto);
            }

            return Json(customerDtos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(CreateCustomerAccountViewModel model)
        {
            var result = false;

            if (ModelState.IsValid)
            {
                // Create new contact 
                List<ContactInfo> contacts = new List<ContactInfo>()
                {
                    new ContactInfo()
                    {
                        TelNo = model.TelNo,
                        Address = model.Address,
                        TelType = ViewBag.TelType
                    }
                };

                uow.ContactInfo.AddRange(contacts);
                uow.Complete();

                // Create new customer 
                Model.Core.DomainModels.Customer customer = new Model.Core.DomainModels.Customer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NationalNo = model.NationalNo,
                    PassportNo = model.PassportNo,
                    Infos = contacts
                };

                uow.Customer.Add(customer);
                uow.Complete();

                PasswordHasher hasher = new PasswordHasher();
                Random random = new Random();

                // Create new user 
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PasswordHash = hasher.HashPassword(model.Password),
                    Person = customer,
                    SecurityStamp = random.Next(100000).ToString()
                };

                uow.User.Add(user);
                uow.Complete();

                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));


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

                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Guid? Id, GetCustomerDto customerDto)
        {
            var result = false;

            if (Id == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid? Id)
        {
            var result = false;






            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}