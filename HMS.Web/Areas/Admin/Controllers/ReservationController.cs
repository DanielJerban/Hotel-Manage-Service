using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Reservation;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class ReservationController : BaseController
    {

        //========================================================
        // NOTE: 
        // 1. To get all customer send a request to Customer/Customer_Read
        //========================================================




        // GET: Admin/Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilterFreeRooms(string fromDate, string toDate)
        {
            var freeRoomInTheTime = uow.Reservation
                .GetEmptyRooms(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate))
                .ToList();

            List<Room_FacilityViewModel> roomsVM = new List<Room_FacilityViewModel>();

            foreach (var room in freeRoomInTheTime)
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

        public ActionResult GetAllFreeRooms()
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

        public ActionResult CreateNewReservation(CreateReserveDto model)
        {
            var result = false;

            if (model.roomsId == null)
            {
                return Json("model", JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                List<Room> selectedRooms = new List<Room>();

                foreach (var Id in model.roomsId)
                {
                    var guid = Guid.Parse(Id);
                    selectedRooms.Add(uow.Room.Find(guid));
                }

                var customer = uow.Customer.Find(model.customerId);

                // Convert string Enum to real Enum
                ReserveStatus status = ToReserveStatus(model.Status);

                Reservation reservation = new Reservation()
                {
                    FromDate = Convert.ToDateTime(model.fromDate),
                    ToDate = Convert.ToDateTime(model.toDate),
                    Customer = customer,
                    Rooms = selectedRooms,
                    Status = status
                };

                uow.Reservation.Add(reservation);
                uow.Complete();

                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateReserveStatus(string _status, Guid reserveId)
        {
            var reserve = uow.Reservation.Find(reserveId);
            ReserveStatus status = ToReserveStatus(_status);

            reserve.Status = status;

            uow.Reservation.Update(reserve);
            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveReservation(Guid reserveId)
        {
            var reserve = uow.Reservation.Find(reserveId);
            uow.Reservation.Remove(reserve);
            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllReservations()
        {
            return Json(uow.Reservation.GetAllReservation(), JsonRequestBehavior.AllowGet);
        }

        #region PartailViews 

        public PartialViewResult _ResreveDetail(Guid Id)
        {
            var reserve = uow.Reservation.Find(Id);

            string status;

            switch (reserve.Status)
            {
                case ReserveStatus.Payed:
                    status = "0";
                    break;
                case ReserveStatus.Temporary:
                    status = "1";
                    break;
                case ReserveStatus.Absolute:
                    status = "2";
                    break;
                default:
                    status = "0";
                    break;
            }

            DetailReserveViewModel vm = new DetailReserveViewModel()
            {
                FromDate = reserve.FromDate.ToString(),
                ToDate = reserve.ToDate.ToString(),
                ReserveStatus = status,
                CustomerName = uow.Reservation.GetReservationCustomerName(Id),
                RoomNumbers = uow.Reservation.GetReservationCustomerRooms(Id)
            };

            return PartialView(vm);
        }

        public PartialViewResult _CustomerPanel()
        {
            return PartialView();
        }

        public PartialViewResult _CreateReservation(Guid Id)
        {
            return PartialView(Id);
        }

        #endregion

        #region Helper Methods 

        public ReserveStatus ToReserveStatus(string _status)
        {
            ReserveStatus status = new ReserveStatus();
            switch (_status)
            {
                case "0":
                    status = ReserveStatus.Payed;
                    break;

                case "1":
                    status = ReserveStatus.Temporary;
                    break;

                case "2":
                    status = ReserveStatus.Absolute;
                    break;

                default:
                    status = ReserveStatus.Payed;
                    break;
            }

            return status;
        }

        #endregion

    }
}