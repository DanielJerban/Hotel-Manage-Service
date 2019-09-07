using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Reservation;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class ReservationController : BaseController
    {
        // GET: Admin/Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFreeRooms(string fromDate, string toDate)
        {
            DateTime date1 = Convert.ToDateTime(fromDate);
            DateTime date2 = Convert.ToDateTime(toDate);

            var freeRoomInTheTime = uow.Reservation.GetEmptyRoom(date1, date2).ToList();

            return Json(freeRoomInTheTime, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllFreeRooms()
        {
            return Json(uow.Reservation.GetAllFreeRooms().ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewReservation(CreateReserveDto model)
        {
            var result = false;

            if (ModelState.IsValid)
            {
                List<Room> selectedRooms = new List<Room>();
                foreach (var Id in model.roomsId)
                {
                    selectedRooms.Add(uow.Room.Find(Id));
                }

                var customer = uow.Customer.Find(model.customerId);

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

        #region PartailViews 

        public PartialViewResult _CreateReserve()
        {
            return PartialView();
        }

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