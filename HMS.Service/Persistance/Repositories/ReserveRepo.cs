using System;
using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using HMS.Model.Core.DTOs.Customer;

namespace HMS.Service.Persistance.Repositories
{
    public class ReserveRepo : Repository<Reserve>, IReserveRepo
    {
        public ReserveRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<Reserve> GetAllReserves()
        {
            var reserves = context.Reserves.Include(c => c.Customer).ToList();
            return reserves;
        }

        public List<Room> GetReservedRooms(Guid Id)
        {
            var reserveRooms = context.Reserve_Rooms.Where(c => c.ReserveId == Id).ToList();
            List<Room> reservedRooms = new List<Room>();

            foreach (var item in reserveRooms)
            {
                var room = context.Rooms.Include(c => c.Facility).SingleOrDefault(c => c.Id == item.RoomId);
                reservedRooms.Add(room);
            }

            return reservedRooms; 
        }
    }
}