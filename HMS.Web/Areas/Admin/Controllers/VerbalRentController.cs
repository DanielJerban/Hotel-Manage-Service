using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class VerbalRentController : BaseController
    {
        // GET: Admin/VerbalRent
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllFreeRooms(string checkIn = null, string checkOut = null)
        {
            // Filter Room
            if (checkIn != null && checkOut != null)
            {
                var filterRoom = uow.Reservation.GetEmptyRooms(Convert.ToDateTime(checkIn), Convert.ToDateTime(checkOut));

                List<Room_FacilityViewModel> roomsVM = new List<Room_FacilityViewModel>();

                foreach (var room in filterRoom)
                {
                    roomsVM.Add(new Room_FacilityViewModel()
                    {
                        RoomNumber = room.RoomNumber,
                        Rate = room.Rate,
                        Description = room.Description,
                        SingleBedCount = room.Facility.SingleBedCount,
                        DoubleBedCount = room.Facility.DoubleBedCount,
                        Entertainment = room.Facility.Entertainment,
                        Id = room.Id
                    });
                }

                return Json(roomsVM, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rooms = uow.Reservation.GetAllFreeRooms().ToList();
                List<Room_FacilityViewModel> roomsVM = new List<Room_FacilityViewModel>();

                foreach (var room in rooms)
                {
                    roomsVM.Add(new Room_FacilityViewModel()
                    {
                        RoomNumber = room.RoomNumber,
                        Rate = room.Rate,
                        Description = room.Description,
                        SingleBedCount = room.Facility.SingleBedCount,
                        DoubleBedCount = room.Facility.DoubleBedCount,
                        Entertainment = room.Facility.Entertainment,
                        Id = room.Id
                    });
                }

                return Json(roomsVM, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult CreateVerbalRent(CreateVerbalRentViewModel model)
        //{
        //    VerbalRoomRent newRent = new VerbalRoomRent()
        //    {
        //        CheckIn = Convert.ToDateTime(model.CheckinDate),
        //        CheckOut = Convert.ToDateTime(model.CheckoutDate),
        //        CustomerId = 
        //    }
        //}

        public ActionResult GetFelowCustomers(string customerId)
        {
            var paretnId = Guid.Parse(customerId);

            return Json(uow.Customer.GetlFelowCustomers(paretnId), JsonRequestBehavior.AllowGet);
        }

        #region Partial

        public PartialViewResult _RentModal()
        {
            return PartialView();
        }

        #endregion

    }
}