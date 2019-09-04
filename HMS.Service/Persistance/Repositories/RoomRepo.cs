using System.Collections.Generic;
using System.Linq;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;

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
    }
}