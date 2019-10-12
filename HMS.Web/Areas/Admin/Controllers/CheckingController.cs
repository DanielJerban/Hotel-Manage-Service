using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Checking;
using HMS.Model.Core.DTOs.Customer;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using MD.PersianDateTime;

namespace HMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CheckingController : BaseController
    {
        // GET: Admin/Checking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomers(string hostId)
        {
            Guid hostGuid = Guid.Parse(hostId);
            var customers = uow.Customer.AllCustomersExceptHost(hostGuid);
            var customerMap = uow.Customer.MapList(customers);

            return Json(customerMap, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFelowCustomersByReserve(string reserveId, string hostId)
        {
            Guid reserveGuid = Guid.Parse(reserveId);
            var customers = uow.Reserve.FelowCustomersByReserve(reserveGuid);

            var customerMap = uow.Customer.MapList(customers);

            return Json(customerMap, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRooms(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrWhiteSpace(fromDate) && string.IsNullOrWhiteSpace(toDate))
            {
                // All Free rooms that does not depend to any time 
                var rooms = uow.Room.GetAllFreeRooms();

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

            DateTime FromDate = PersianDateTime.Parse(fromDate).ToDateTime();
            DateTime ToDate = PersianDateTime.Parse(toDate).ToDateTime();

            // Free Rooms depend on Reserve and checking FromDate and ToDate 
            var freeRooms =
                uow.Room.AllFreeRoomsInDateRange(FromDate, ToDate).ToList();

            List<Room_FacilityViewModel> RoomsVM = new List<Room_FacilityViewModel>();

            foreach (var room in freeRooms)
            {
                RoomsVM.Add(new Room_FacilityViewModel()
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

            return Json(RoomsVM, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateChecking(CreateCheckingDto model)
        {
            DateTime fromDate = PersianDateTime.Parse(model.FromDate).ToDateTime();
            DateTime toDate = PersianDateTime.Parse(model.ToDate).ToDateTime();
            Guid hostId = Guid.Parse(model.HostId);
            Guid roomId = Guid.Parse(model.RoomId);

            // Create Checking 
            Checking checking = new Checking()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = hostId,
                RoomId = roomId
            };
            uow.Checking.Add(checking);

            // Setting passenger for the reserve and the host
            foreach (var id in model.PassengersId)
            {
                Guid customerGuid = Guid.Parse(id);

                Passenger passenger = new Passenger()
                {
                    CustomerId = customerGuid,
                    CheckingId = checking.Id
                };
                uow.Passenger.Add(passenger);
            }

            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCheckingByReserve(CreateCheckingDto model, string reserveId)
        {
            Guid reserveGuid = Guid.Parse(reserveId);
            DateTime fromDate = PersianDateTime.Parse(model.FromDate).ToDateTime();
            DateTime toDate = PersianDateTime.Parse(model.ToDate).ToDateTime();
            Model.Core.DomainModels.Customer host = uow.Reserve.GetHost(reserveGuid);
            Guid roomGuid = Guid.Parse(model.RoomId);

            // Create Checking 
            Checking checking = new Checking()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = host.Id,
                RoomId = roomGuid,
                ReserveId = reserveGuid
            };
            uow.Checking.Add(checking);

            // Setting passenger for the reserve and the host
            foreach (var id in model.PassengersId)
            {
                Guid customerGuid = Guid.Parse(id);

                Passenger passenger = new Passenger()
                {
                    CustomerId = customerGuid,
                    CheckingId = checking.Id
                };
                uow.Passenger.Add(passenger);
            }

            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCheckings()
        {
            return Json(uow.Checking.GetCheckings(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalcualteReservePrice(string fromDate, string toDate, string roomId)
        {
            double price = 0;

            if (roomId == null)
            {
                return Json(price.ToString(), JsonRequestBehavior.AllowGet);
            }

            PersianDateTime persianFromDate = PersianDateTime.Parse(fromDate);
            DateTime FromDate = persianFromDate.ToDateTime();

            PersianDateTime persianToDate = PersianDateTime.Parse(toDate);
            DateTime ToDate = persianToDate.ToDateTime();

            int dateRange = ToDate.Subtract(FromDate).Days + 1;

            Guid roomGuid = Guid.Parse(roomId);
            var room = uow.RoomPrice.Get(c => c.RoomId == roomGuid && c.Date == FromDate).SingleOrDefault();

            if (room == null) // The room does not have any price set
            {
                RoomPrice roomPrice = new RoomPrice()
                {
                    RoomId = roomGuid,
                    Date = FromDate,
                    Price = 100000
                };

                uow.RoomPrice.Add(roomPrice);
                uow.Complete();

                price += (roomPrice.Price * dateRange);
            }
            else
            {
                price += (room.Price * dateRange);
            }


            return Json(price.ToString(), JsonRequestBehavior.AllowGet);
        }



        #region Parital views

        public PartialViewResult _CheckingModal()
        {
            return PartialView();
        }

        public PartialViewResult _CreateFellowCustomer()
        {
            return PartialView();
        }

        public PartialViewResult _ShowCheckings()
        {
            return PartialView();
        }

        public PartialViewResult _CheckingFellowCustomers(string CheckingId)
        {
            Guid checkingGuid = Guid.Parse(CheckingId);

            var passengers = uow.Passenger.CheckingPassengers(checkingGuid);

            return PartialView(passengers);
        }

        public PartialViewResult _CheckLastReserve()
        {
            return PartialView();
        }

        public PartialViewResult _ReservedRooms()
        {
            return PartialView();
        }

        public ActionResult _CheckingForReserve()
        {
            return PartialView();
        }
        #endregion
    }
}