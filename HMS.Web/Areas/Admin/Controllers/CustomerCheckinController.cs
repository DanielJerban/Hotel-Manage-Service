using HMS.Model.Core.DTOs.Customer;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class CustomerCheckinController : BaseController
    {

        // ========================================
        // Notes: 
        // 1. for creating new customer send an ajax to Customer/Customer_Create
        // 2. to Get All Customers send an ajax to Customer/Customer_Read
        // 3. to Get back all free rooms send an ajax to Reservation/GetAllFreeRooms
        // ========================================




        // GET: Admin/CustomerCheckin
        public ActionResult Index()
        {
            return View();
        }

        // Get all empty rooms 
        public ActionResult GetAllFreeRooms()
        {
            return Json(uow.Reservation.GetAllFreeRooms().ToList(), JsonRequestBehavior.AllowGet);
        }

        // Get the rooms that are not reserved and not rented either 
        public ActionResult GetAllFreeRooms(string fromDate , string toDate)
        {
            throw new NotImplementedException();
        }
    }
}