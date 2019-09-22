using System;
using System.Collections.Generic;
using System.Linq;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using MD.PersianDateTime;

namespace HMS.Service.Persistance.Repositories
{
    public class RoomRepo : Repository<Room> , IRoomRepo
    {
        public RoomRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<Room_FacilityViewModel> GetRooms()
        {
            var rooms = context.Rooms.Include(c => c.Facility).ToList();
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

            return roomsVM;
        }


        public List<Room> AllFreeRoomsInDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Room> rooms = context.Rooms.Include(c => c.Facility).ToList();

            var reserves = context.Reserves
                .Where(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.Status == Status.Canceled
                ).ToList();

            var checkings = context.Checkings
                .Where(c =>
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
                var reserveRooms = context.Reserve_Rooms.Where(c => c.ReserveId == item.Id).ToList();
                foreach (var reserveRoom in reserveRooms)
                {
                    rooms.Remove(reserveRoom.Room);
                }
            }

            return rooms; 
        }

        public List<Room> GetAllFreeRooms()
        {
            List<Room> rooms = context.Rooms.Include(c => c.Facility).ToList();
            List<Room> freeRooms = new List<Room>(rooms);

            var reserveRoom = context.Reserve_Rooms.ToList();
            foreach (var item in reserveRoom)
            {
                foreach (var room in rooms)
                {
                    if (item.RoomId == room.Id)
                    {
                        freeRooms.Remove(room);
                    }
                }
            }

            var checkings= context.Checkings.ToList();
            foreach (var item in checkings)
            {
                foreach (var room in rooms)
                {
                    if (item.RoomId == room.Id)
                    {
                        freeRooms.Remove(room);
                    }
                }
            }

            return freeRooms;
        }

    }
}