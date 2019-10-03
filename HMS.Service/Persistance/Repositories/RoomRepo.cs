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
    public class RoomRepo : Repository<Room>, IRoomRepo
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

        public WeekHeaderRoomsListsViewModel GetRoomsInDateRange(DateTime fromDate, DateTime toDate)
        {
            List<WeeklyRoomViewModel> rooms = new List<WeeklyRoomViewModel>();

            var reserves = context.Reserves
                .Where(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate
                ).ToList();

            var checkings = context.Checkings.Include(c => c.Room)
                .Where(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate
                ).ToList();


            List<WeeklyRoomViewModel> Saturday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Sunday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Monday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Teusday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Wendsday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Tursday = new List<WeeklyRoomViewModel>();
            List<WeeklyRoomViewModel> Frieday = new List<WeeklyRoomViewModel>();


            // Add checked rooms to list
            foreach (var item in checkings)
            {

                PersianDateTime startingDate = new PersianDateTime(item.FromDate);
                PersianDateTime endingDate = new PersianDateTime(item.ToDate);


                var range = item.ToDate.Subtract(item.FromDate).Days + 1;
                int day = Convert.ToInt32(startingDate.PersianDayOfWeek);


                for (int i = 0; i < range; i++)
                {
                    var nextDay = startingDate.AddDays(i);

                    switch (day)
                    {
                        case 0:
                            Saturday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 1:
                            Sunday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 2:
                            Monday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 3:
                            Teusday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 4:
                            Wendsday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 5:
                            Tursday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                        case 6:
                            Frieday.Add(new WeeklyRoomViewModel()
                            {
                                Id = item.RoomId,
                                RoomNumber = item.Room.RoomNumber,
                                RoomStatus = Status.Payed,
                                Date = nextDay.ToDateTime(),
                                CheckingId = item.Id,
                                ReserveId = null
                            });
                            break;
                    }

                    if (day == 6)
                    {
                        day = 0;
                    }
                    else
                    {
                        day++;
                    }
                }

            }

            // Add Reserved rooms to list  
            foreach (var item in reserves)
            {
                PersianDateTime startingDate = new PersianDateTime(item.FromDate);
                PersianDateTime endingDate = new PersianDateTime(item.ToDate);


                var range = item.ToDate.Subtract(item.FromDate).Days + 1;

                var reserveRooms = context.Reserve_Rooms.Include(c => c.Room).Where(c => c.ReserveId == item.Id).ToList();

                var roomStatus = item.Status;
                var reserveId = item.Id;

                foreach (var reserveRoom in reserveRooms)
                {
                    int day = Convert.ToInt32(startingDate.PersianDayOfWeek);

                    for (int i = 0; i < range; i++)
                    {
                        var nextDay = startingDate.AddDays(i);

                        switch (day)
                        {
                            case 0:
                                Saturday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 1:
                                Sunday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 2:
                                Monday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 3:
                                Teusday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 4:
                                Wendsday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 5:
                                Tursday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                            case 6:
                                Frieday.Add(new WeeklyRoomViewModel()
                                {
                                    Id = reserveRoom.RoomId,
                                    Date = nextDay,
                                    RoomNumber = reserveRoom.Room.RoomNumber,
                                    RoomStatus = roomStatus,
                                    ReserveId = reserveId,
                                    CheckingId = null
                                });
                                break;
                        }

                        if (day == 6)
                        {
                            day = 0;
                        }
                        else
                        {
                            day++;
                        }
                    }
                }
            }


            WeekHeaderRoomsListsViewModel vm = new WeekHeaderRoomsListsViewModel()
            {
                Saturday = Saturday,
                Sunday = Sunday,
                Monday = Monday,
                Tuesday = Teusday,
                Wednesday = Wendsday,
                Thursday = Tursday,
                Friday = Frieday
            };

            return vm;
        }

        public List<Room> AllFreeRoomsInDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Room> rooms = context.Rooms.Include(c => c.Facility).ToList();

            var reserves = context.Reserves
                .Where(c =>
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate >= toDate && c.ToDate >= fromDate ||
                    c.FromDate <= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate ||
                    c.FromDate >= fromDate && c.FromDate <= toDate && c.ToDate <= toDate && c.ToDate >= fromDate
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

            var checkings = context.Checkings.ToList();
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