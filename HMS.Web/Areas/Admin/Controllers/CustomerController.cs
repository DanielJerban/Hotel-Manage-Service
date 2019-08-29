using HMS.Model.Core.DTOs.Customer;
using HMS.Web.Controllers.Base;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Customer_Read()
        {
            var customers = uow.Customer.GetAll();
            var customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PassportNo = customer.PassportNo,
                    NationalNo = customer.NationalNo
                });
            }

            return Json(customerDtos, JsonRequestBehavior.AllowGet);
        }

    }
}