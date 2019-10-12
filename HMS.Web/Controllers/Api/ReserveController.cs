using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Reserve;
using HMS.Model.Core.ViewModels;
using HMS.Service.Core;
using HMS.Service.Persistance;
using HMS.Web.Jobs;
using HMS.Web.Models;
using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace HMS.Web.Controllers.Api
{
    public class ReserveController : ApiController
    {
        protected readonly IUnitOfWork uow;

        public ReserveController()
        {
            uow = new UnitOfWork(new ApplicationDbContext());
        }

        /// <summary>
        /// Create a temporary reserve with 24 hours expire time 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult CreateReserve(CreateReserveApiDto model)
        {
            #region Create Temporary Reserve

            DateTime fromDate = PersianDateTime.Parse(model.FromDate).ToDateTime();
            DateTime toDate = PersianDateTime.Parse(model.ToDate).ToDateTime();
            Guid hostId = Guid.Parse(model.HostId);

            List<Room> freeRoomsInDateRange = uow.Room.AllFreeRoomsInDateRange(fromDate, toDate);

            // Creating new reserve
            Reserve reserve = new Reserve()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = hostId,
                Status = Status.Temporary
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
                var room = uow.Room.Find(roomGuid);

                if (!freeRoomsInDateRange.Contains(room))
                {
                    return BadRequest("The selected room is not empty in this date range");
                }


                Reserve_Room reserve_room = new Reserve_Room()
                {
                    ReserveId = reserve.Id,
                    RoomId = roomGuid
                };
                uow.Reserve_Room.Add(reserve_room);
            }

            uow.Complete();

            #endregion

            #region Calculate price

            double price = 0;

            PersianDateTime persianFromDate = PersianDateTime.Parse(model.FromDate);
            DateTime FromDate = persianFromDate.ToDateTime();

            PersianDateTime persianToDate = PersianDateTime.Parse(model.ToDate);
            DateTime ToDate = persianToDate.ToDateTime();

            int dateRange = ToDate.Subtract(FromDate).Days + 1;

            foreach (var id in model.RoomsId)
            {
                Guid roomGuid = Guid.Parse(id);
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
            }

            price /= 2;

            #endregion

            #region Remove Reserve after 24 hours 

            // Quartz
            Task.Run(() => ReserveJobs.Start(reserve.Id, DateTime.Now.AddDays(1)));

            #endregion

            var totalPrice = price;
            var refrenceCode = reserve.Id;

            return Ok(new { totalPrice, refrenceCode});
        }

        public IHttpActionResult ConfirmReserve(string refrenceCode)
        {
            Guid reserveId = Guid.Parse(refrenceCode);
            var reserve = uow.Reserve.Find(reserveId);
            if (reserve == null)
            {
                return Ok("There is no reserve with that refrece code!\nCheck your refrence code again.");
            }

            reserve.Status = Status.Absolute;
            uow.Reserve.Update(reserve);
            uow.Complete();

            return Ok("Reserve status updated to absolute status.");
        }
    }
}
