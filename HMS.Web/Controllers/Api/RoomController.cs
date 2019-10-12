using HMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using HMS.Model.Core.DomainModels;
using MD.PersianDateTime;
using HMS.Service.Core;
using HMS.Service.Persistance;
using HMS.Web.Models;

namespace HMS.Web.Controllers.Api
{
    public class RoomController : ApiController
    {
        protected readonly IUnitOfWork uow;

        public RoomController()
        {
            uow = new UnitOfWork(new ApplicationDbContext());
        }


        /// <summary>
        /// Gets the count of free rooms in date range 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult FreeRoomsCount(string FromDate, string ToDate)
        {
            PersianDateTime persianFromDate = PersianDateTime.Parse(FromDate);
            PersianDateTime persianToDate = PersianDateTime.Parse(ToDate);

            DateTime fromDate = persianFromDate.ToDateTime();
            DateTime toDate = persianToDate.ToDateTime();

            List<Room> rooms = uow.Room.GetAll().ToList();

            var reserves = uow.Reserve
                .Get(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate
                ).ToList();

            var checkings = uow.Checking
                .Get(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate
                ).ToList();

            // Remove taken rooms from checking 
            foreach (var item in checkings)
            {
                rooms.Remove(item.Room);
            }

            // Remove taken rooms from reserves 
            foreach (var item in reserves)
            {
                // Get Reserve_Room Junction by Id 
                var reserveRooms = uow.Reserve_Room.Get(c => c.ReserveId == item.Id).ToList();
                foreach (var reserveRoom in reserveRooms)
                {
                    rooms.Remove(reserveRoom.Room);
                }
            }

            return Ok(rooms.Count);
        }
    }
}
