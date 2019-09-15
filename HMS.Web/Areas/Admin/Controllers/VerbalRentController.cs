using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using MD.PersianDateTime;

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
                var filterRoom = uow.Reservation
                    .GetEmptyRooms(PersianDateTime.Parse(checkIn).ToDateTime(), PersianDateTime.Parse(checkOut).ToDateTime());

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

        public JsonResult CreateVerbalRent(CreateVerbalRentViewModel model)
        {
            var parentId = Guid.Parse(model.ParentId);

            var passengers = new List<Passenger>();

            // Create the passengers 
            foreach (var Id in model.FelowCustomer)
            {
                var customerId = Guid.Parse(Id);
                Passenger newPassenger = new Passenger()
                {
                    CustomerId = customerId
                };

                uow.Passenger.Add(newPassenger);
            }

            var room = new List<Room>();

            // Get Room Entity
            foreach (var roomId in model.Rooms)
            {
                var guid = Guid.Parse(roomId);

                room.Add(uow.Room.Get(c => c.Id == guid).SingleOrDefault());
            }

            var checkinDate = PersianDateTime.Parse(model.CheckinDate).ToDateTime();
            var checkoutDate = PersianDateTime.Parse(model.CheckoutDate);

            VerbalRoomRent newRent = new VerbalRoomRent()
            {
                CheckIn = checkinDate,
                CheckOut = checkoutDate,
                AbsoluteCheckOut = null,
                Passengers = passengers,
                CustomerId = parentId,
                Rooms = room
            };

            uow.VerbalRent.Add(newRent);

            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

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

        public PartialViewResult _ShowRentModal()
        {
            return PartialView();
        }

        #endregion

    }
}