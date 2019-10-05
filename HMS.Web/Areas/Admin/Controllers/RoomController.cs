using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Library.Helpers;
using MD.PersianDateTime;
using System.Data;
using System.Data.Entity;
using HMS.Model.Core.DTOs.Customer;
using HMS.Model.Core.DTOs.Room;

namespace HMS.Web.Areas.Admin.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class RoomController : BaseController
    {
        #region Views

        // GET: Admin/Room
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RoomStatusPerWeek()
        {
            return View();
        }

        #endregion

        #region Room Operations

        public ActionResult Room_Read()
        {
            return Json(uow.Room.GetRooms(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Room_Create(Room_FacilityViewModel model, Guid hotelId)
        {
            if (ModelState.IsValid)
            {
                List<RoomImage> roomImages = new List<RoomImage>();

                foreach (var item in ImageList.PostedFiles)
                {
                    var image = SaveImage(item);
                    roomImages.Add(image);
                }

                var facility = new RoomFacility()
                {
                    SingleBedCount = model.SingleBedCount,
                    DoubleBedCount = model.DoubleBedCount,
                    Entertainment = model.Entertainment
                };
                uow.RoomFacility.Add(facility);


                var room = new Room()
                {
                    RoomNumber = model.RoomNumber,
                    Rate = model.Rate,
                    Description = model.Description,
                    Facility = facility,
                    Images = roomImages,
                    Hotel = uow.Hotel.Get(c => c.Id == hotelId).SingleOrDefault()
                };
                uow.Room.Add(room);

                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Room_Destroy(Guid Id)
        {
            uow.Room.Remove(Id);
            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void GetImages(IEnumerable<HttpPostedFileBase> files)
        {

            foreach (var item in files)
            {
                // Skip the empty file 
                if (item == null)
                {
                    continue;
                }

                var postedImage = ImageList.PostedFiles.SingleOrDefault(c => c.FileName == item.FileName);

                if (postedImage == null)
                {
                    ImageList.PostedFiles.Add(item);
                }
            }
        }

        private RoomImage SaveImage(HttpPostedFileBase file)
        {
            // Save file in the directory 
            // Check if the directory exists 
            if (!Directory.Exists(Server.MapPath("~/App_Data/Uploads")))
            {
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Uploads"));
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileLocation = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);

            file.SaveAs(fileLocation);

            // Checking if the image path exist in the database 
            var imageExist = uow.RoomImage.Get(c => c.Path == fileLocation).SingleOrDefault();
            if (imageExist == null)
            {
                // Save the path to database 
                RoomImage img = new RoomImage()
                {
                    Path = fileLocation,
                    FileName = fileName,
                    TotalFileSize = file.ContentLength
                };
                uow.RoomImage.Add(img);
                uow.Complete();
            }

            var roomImage = uow.RoomImage.Get(c => c.Path == fileLocation).SingleOrDefault();

            return roomImage;
        }

        #endregion

        #region Weekly Persian Calender

        public JsonResult GetCurrentDay(int index = 0)
        {
            string year = MyCalender.getYearFromHeader(index);
            int currentDay = MyCalender.getCurrentDay();
            List<string> weekHeaderDate = MyCalender.getDayFromHeader(index);

            string monthNameInNumber = MyCalender.getMonthFromHeader(index);
            string monthName = MyCalender.ConvertMonth(monthNameInNumber);

            var roomData = uow.Room.GetRoomsInDateRange(MyCalender.GetCurrentWeekFirstDayDate(),
                MyCalender.GetCurrentWeekLastDayDate());



            return Json(new { monthName, year, currentDay, weekHeaderDate, roomData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWeekHeader(int index = 0)
        {
            string monthNameInNumber = MyCalender.getMonthFromHeader(index);
            string monthName = MyCalender.ConvertMonth(monthNameInNumber);

            string year = MyCalender.getYearFromHeader(index);

            List<string> weekHeaderDate = MyCalender.getDayFromHeader(index);

            PersianDateTime WeekFirstDay = MyCalender.GetCurrentWeekFirstDayDate().AddDays(7 * index);
            PersianDateTime WeekLastDay = MyCalender.GetCurrentWeekLastDayDate().AddDays(7 * index);

            var roomData = uow.Room.GetRoomsInDateRange(WeekFirstDay, WeekLastDay);

            return Json(new { monthName, year, weekHeaderDate, roomData }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get All Reserved and checked in rooms and their status 
        /// </summary>
        /// <returns>List of WeeklyRoom ViewModel</returns>


        #endregion

        #region Customer

        public JsonResult CustomersInHotel()
        {
            var passengers = uow.Passenger.GetAll().Include(c => c.Customer);

            List<CustomerInHotelDto> customerDtos = new List<CustomerInHotelDto>();

            foreach (var passenger in passengers)
            {
                CustomerInHotelDto dto = new CustomerInHotelDto()
                {
                    FirstName = passenger.Customer.FirstName ?? "",
                    LastName = passenger.Customer.LastName ?? "",
                    NationalNo = passenger.Customer.NationalNo ?? "",
                    PassportNo = passenger.Customer.PassportNo ?? "",
                    CustomerId = passenger.Customer.Id,
                    PassengerId = passenger.Id
                };


                if (customerDtos.Find(c => c.CustomerId == dto.CustomerId) == null)
                {
                    customerDtos.Add(dto);
                }
            }

            return Json(customerDtos.OrderBy(c => c.LastName), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Room Pricing

        public ActionResult SetRoomPrice(RoomPriceDto model)
        {
            PersianDateTime persianFromDate = PersianDateTime.Parse(model.FromDate);
            PersianDateTime persianToDate = PersianDateTime.Parse(model.ToDate);
            Guid roomGuid = Guid.Parse(model.RoomId);

            DateTime fromDate = persianFromDate.ToDateTime();
            DateTime toDate = persianToDate.ToDateTime();

            var dateRange = toDate.Subtract(fromDate).Days + 1;

            List<RoomPrice> roomPriceList = new List<RoomPrice>();

            for (int i = 0; i < dateRange; i++)
            {
                var nextDay = persianFromDate.AddDays(i);

                DateTime nextDateTime = nextDay.ToDateTime();

                var roomPriceDB = uow.RoomPrice.Get(c => c.Date == nextDateTime && c.RoomId == roomGuid)
                    .SingleOrDefault();

                if (roomPriceDB == null)
                {
                    RoomPrice roomPrice = new RoomPrice()
                    {
                        Date = nextDay.ToDateTime(),
                        Price = model.Price,
                        RoomId = roomGuid
                    };

                    uow.RoomPrice.Add(roomPrice);

                    roomPriceList.Add(roomPrice);
                }
            }

            if (roomPriceList.Count < 1)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Partial Views 

        public PartialViewResult _CreateRoom()
        {
            ViewBag.HotelId = new SelectList(uow.Hotel.GetAll(), "Id", "Name");
            return PartialView();
        }

        public PartialViewResult _ReserveDetail(string reserveId, string roomId)
        {
            Guid reserveGuid = Guid.Parse(reserveId);
            Guid roomGuid = Guid.Parse(roomId);


            string fromDate;
            string toDate;

            string reserveNumber;
            string reserveStatus;
            string hostName;
            List<string> fellowCustomers = new List<string>();

            //var reserve = uow.Reserve.Get(c => c.Reserve_Reserve_Rooms.Contains(reserveRoom))
            //    .Include(c => c.Customer).Include(c => c.Fellows).Include(c => c.Reserve_Reserve_Rooms).SingleOrDefault();

            var reserve = uow.Reserve.Get(c => c.Id == reserveGuid).Include(c => c.Customer).SingleOrDefault();

            hostName = reserve.Customer.FirstName + " " + reserve.Customer.LastName;

            PersianDateTime persianFromDate = new PersianDateTime(reserve.FromDate);
            PersianDateTime persianToDate = new PersianDateTime(reserve.ToDate);

            fromDate = persianFromDate.ToShortDateString();
            toDate = persianToDate.ToShortDateString();

            reserveNumber = reserve.Number.ToString();

            //var fellows = uow.Fellow.Get(c => c.ReserveId == reserveGuid).ToList();

            var fellows = uow.Fellow.Get(c => c.ReserveId == reserveGuid).Include(c => c.Customer).ToList();

            foreach (var fellow in fellows)
            {
                fellowCustomers.Add(fellow.Customer.FirstName + " " + fellow.Customer.LastName);
            }

            switch (reserve.Status)
            {
                case Status.Payed:
                    reserveStatus = "پرداخت شده";
                    break;
                case Status.Absolute:
                    reserveStatus = "قطعی";
                    break;
                case Status.Temporary:
                    reserveStatus = "موقت";
                    break;
                case Status.Canceled:
                    reserveStatus = "کنسل شده";
                    break;
                default:
                    reserveStatus = "پرداخت شده";
                    break;
            }

            RoomReserveDetailDto dto = new RoomReserveDetailDto()
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReserveNumber = reserveNumber,
                ReserveStatus = reserveStatus,
                HostName = hostName,
                FellowsName = fellowCustomers
            };

            return PartialView(dto);
        }

        public PartialViewResult _CheckingDetail(string checkingId, string roomId)
        {
            Guid roomGuid = Guid.Parse(roomId);
            Guid checkingGuid = Guid.Parse(checkingId);

            var checking = uow.Checking.Get(c => c.Id == checkingGuid).Include(c => c.Customer).SingleOrDefault();

            string checkingNo = checking.Number.ToString();

            PersianDateTime persianFromDate = new PersianDateTime(checking.FromDate);
            PersianDateTime persianToDate = new PersianDateTime(checking.ToDate);

            string fromDate = persianFromDate.ToShortDateString();
            string toDate = persianToDate.ToShortDateString();

            string hostName = checking.Customer.FirstName + " " + checking.Customer.LastName;

            var passengers = uow.Passenger.Get(c => c.CheckingId == checkingGuid).Include(c => c.Customer).ToList();

            List<string> passengersName = new List<string>();

            foreach (var passenger in passengers)
            {
                passengersName.Add(passenger.Customer.FirstName + " " + passenger.Customer.LastName);
            }

            RoomCheckingDetailDto dto = new RoomCheckingDetailDto()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CheckingNumber = checkingNo,
                HostName = hostName,
                Passengers = passengersName
            };

            return PartialView(dto);
        }

        public PartialViewResult _PassengerDetails(string passengerId)
        {
            Guid passengerGuid = Guid.Parse(passengerId);
            var passenger = uow.Passenger.Get(c => c.Id == passengerGuid).Include(c => c.Checking).SingleOrDefault();

            var checkingId = passenger.CheckingId;
            var checking = uow.Checking.Get(c => c.Id == checkingId).Include(c => c.Customer).Include(c => c.Room).SingleOrDefault();

            PersianDateTime persianFromDate = new PersianDateTime(checking.FromDate);
            PersianDateTime persianToDate = new PersianDateTime(checking.ToDate);

            string fromDate = persianFromDate.ToShortDateString();
            string toDate = persianToDate.ToShortDateString();
            string roomNumber = checking.Room.RoomNumber;


            CustomerInHotelDetailDto dto = new CustomerInHotelDetailDto()
            {
                HostName = checking.Customer.FirstName + " " + checking.Customer.LastName,
                FromDate = fromDate,
                ToDate = toDate,
                RoomsNumber = roomNumber,
                CustomerId = passenger.CustomerId
            };

            return PartialView(dto);
        }

        public PartialViewResult _CreateContact()
        {
            return PartialView();
        }

        public PartialViewResult _RoomPrice()
        {
            return PartialView();
        }

        #endregion
    }



    // Static class to save the upload files in the property 
    public static class ImageList
    {
        public static List<HttpPostedFileBase> PostedFiles = new List<HttpPostedFileBase>();
    }
}