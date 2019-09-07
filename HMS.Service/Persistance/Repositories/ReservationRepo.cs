using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HMS.Service.Persistance.Repositories
{
    public class ReservationRepo : Repository<Reservation>, IReservationRepo
    {
        public ReservationRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<Room> GetAllFreeRooms()
        {
            var rooms = context.Rooms.Where(c => c.Reservation == null).ToList();

            return rooms; 
        }

        public List<Room> GetEmptyRoom(DateTime fromDate, DateTime toDate)
        {
            // First Filter => not reserved rooms 
            var rooms = context.Rooms.ToList();

            // All reservations in a specific time 
            var reservations = context.Reservations.Where(c => c.FromDate < fromDate && c.ToDate > toDate).ToList();

            // Remove the full rooms from all rooms 
            foreach (var reserv in reservations)
            {
                foreach (var reserveredRoom in reserv.Rooms)
                {
                    rooms.Remove(reserveredRoom);
                }
            }

            return rooms;
        }

        public string GetReservationCustomerName(Guid Id)
        {
            var customerReserve = context.Reservations.Include(c => c.Customer).FirstOrDefault(c => c.Id == Id);

            return customerReserve.Customer.FirstName + ' ' + customerReserve.Customer.LastName;
        }

        public List<string> GetReservationCustomerRooms(Guid Id)
        {
            var customerReservedRooms = context.Reservations.Include(c => c.Customer).FirstOrDefault(c => c.Id == Id).Rooms;

            List<string> RoomsNumber = new List<string>();

            foreach (var item in customerReservedRooms)
            {
                RoomsNumber.Add(item.RoomNumber);
            }

            return RoomsNumber;
        }
    }
}
