using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Model.Core.DTOs.Customer;
using MD.PersianDateTime;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;
using System.Globalization;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class ReserveController : BaseController
    {
        // GET: Admin/Reserve
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateReserve(CreateReserveViewModel model)
        {
            DateTime fromDate = PersianDateTime.Parse(model.FromDate).ToDateTime();
            DateTime toDate = PersianDateTime.Parse(model.ToDate).ToDateTime();
            Guid hostId = Guid.Parse(model.HostId);

            Status status = new Status();
            switch (model.Status)
            {
                case "0":
                    status = Status.Payed;
                    break;
                case "1":
                    status = Status.Absolute;
                    break;
                case "2":
                    status = Status.Temporary;
                    break;
                case "3":
                    status = Status.Canceled;
                    break;
            }

            // Creating new reserve
            Reserve reserve = new Reserve()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = hostId,
                Status = status
            };
            uow.Reserve.Add(reserve);

            // Setting fellow for the reserve and the host 
            foreach (var fellowId in model.FellowsId)
            {
                Guid fellowGuid = Guid.Parse(fellowId);

                Fellow fellow = new Fellow()
                {
                    ReserveId = reserve.Id,
                    CustomerId = fellowGuid
                };
                uow.Fellow.Add(fellow);
            }

            // Setting Rooms for a reserve 
            foreach (var roomId in model.RoomsId)
            {
                Guid roomGuid = Guid.Parse(roomId);

                Reserve_Room reserve_room = new Reserve_Room()
                {
                    ReserveId = reserve.Id,
                    RoomId = roomGuid
                };
                uow.Reserve_Room.Add(reserve_room);
            }

            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomersExceptHost(string HostId)
        {
            Guid hostId = Guid.Parse(HostId);

            var allCustomers = uow.Customer.GetAll().ToList();
            var customers = new List<Model.Core.DomainModels.Customer>();

            foreach (var customer in allCustomers)
            {
                if (customer.Id == hostId)
                {
                    continue;
                }
                else
                {
                    customers.Add(customer);
                }
            }

            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    CreatedDate = customer.CreatedDate,
                    FirstName = customer.FirstName ?? "",
                    LastName = customer.LastName ?? "",
                    NationalNo = customer.NationalNo ?? "",
                    PassportNo = customer.PassportNo ?? "",
                    Id = customer.Id
                });
            }

            return Json(customerDtos.OrderByDescending(c => c.CreatedDate), JsonRequestBehavior.AllowGet);
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

        public ActionResult GetReserves()
        {
            var reserves = uow.Reserve.GetAllReserves();
            List<ReserveViewModel> vm = new List<ReserveViewModel>();

            foreach (var reserve in reserves)
            {
                string status;
                switch (reserve.Status)
                {
                    case Status.Payed:
                        status = "پرداخت شده";
                        break;
                    case Status.Absolute:
                        status = "قطعی";
                        break;
                    case Status.Temporary:
                        status = "موقت";
                        break;
                    case Status.Canceled:
                        status = "کنسل شده";
                        break;
                    default:
                        status = "پرداخت شده";
                        break;
                }

                PersianCalendar pc = new PersianCalendar();
                string FromDate = pc.GetDayOfMonth(reserve.FromDate).ToString() + "/"
                                + pc.GetMonth(reserve.FromDate).ToString() + "/"
                                + pc.GetYear(reserve.FromDate).ToString();

                string ToDate = pc.GetDayOfMonth(reserve.ToDate).ToString() + "/"
                                + pc.GetMonth(reserve.ToDate).ToString() + "/"
                                + pc.GetYear(reserve.ToDate).ToString();

                vm.Add(new ReserveViewModel()
                {
                    ReserveId = reserve.Id,
                    FromDate = FromDate,
                    ToDate = ToDate,
                    CustomerName = reserve.Customer.FirstName + ' ' + reserve.Customer.LastName,
                    Status = status,
                    Number = reserve.Number
                });
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReservedRoom(string Id)
        {
            Guid reserveGuid = Guid.Parse(Id);

            var reservedRooms = uow.Reserve.GetReservedRooms(reserveGuid);

            List<Room_FacilityViewModel> roomsVM = new List<Room_FacilityViewModel>();

            foreach (var room in reservedRooms)
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

        public ActionResult UpdateReserveStatus(string status, string reserveId)
        {
            Guid reserveGuid = Guid.Parse(reserveId);
            Status Status = new Status();


            switch (status)
            {
                case "0":
                    Status = Status.Payed;
                    break;
                case "1":
                    Status = Status.Absolute;
                    break;
                case "2":
                    Status = Status.Temporary;
                    break;
                case "3":
                    Status = Status.Canceled;
                    break;
                default:
                    Status = Status.Payed;
                    break;
            }

            var reserve = uow.Reserve.Find(reserveGuid);
            reserve.Status = Status;
            uow.Reserve.Update(reserve);
            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveReserveRoom(string reserveId, string roomId)
        {
            Guid reserveGuid = Guid.Parse(reserveId);
            Guid roomGuid = Guid.Parse(roomId);

            var reserveRoom = uow.Reserve_Room.Get(c => c.RoomId == roomGuid && c.ReserveId == reserveGuid).SingleOrDefault();

            if (reserveRoom != null)
            {
                uow.Reserve_Room.Remove(reserveRoom);
                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #region Partial Views 

        public PartialViewResult _ReserveModal()
        {
            return PartialView();
        }

        public PartialViewResult _CreateFellowCustomer()
        {
            return PartialView();
        }

        public PartialViewResult _GetReserves()
        {
            return PartialView();
        }

        public PartialViewResult _ReserveDetail()
        {
            return PartialView();
        }

        #endregion
    }
}