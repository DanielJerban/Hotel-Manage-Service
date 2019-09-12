using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HMS.Model.Core.ViewModels;
using System.Globalization;

namespace HMS.Service.Persistance.Repositories
{
    public class ReservationRepo : Repository<Reservation>, IReservationRepo
    {
        public ReservationRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<Room> GetAllFreeRooms()
        {
            var rooms = context.Rooms.Include(c => c.Facility).Where(c => c.Reservation == null && c.VerbalRoomRent == null).ToList();

            return rooms;
        }

        /// <summary>
        /// Gets all Reservations till now
        /// </summary>
        public List<ReservationViewModel> GetAllReservation()
        {
            var reservation = context.Reservations.Include(c => c.Customer).ToList();

            List<ReservationViewModel> VMs = new List<ReservationViewModel>();

            foreach (var item in reservation)
            {
                List<string> roomsNumber = new List<string>();
                foreach (var room in item.Rooms)
                {
                    roomsNumber.Add(room.RoomNumber);
                }

                string status;

                switch (item.Status)
                {
                    case ReserveStatus.Payed:
                        status = "پرداخت شده";
                        break;
                    case ReserveStatus.Temporary:
                        status = "موقت";
                        break;
                    case ReserveStatus.Absolute:
                        status = "قطعی";
                        break;
                    default:
                        status = "پرداخت شده";
                        break;
                }

                VMs.Add(new ReservationViewModel()
                {
                    FromDate = item.FromDate,
                    ToDate = item.ToDate,
                    RoomsNumber = roomsNumber,
                    CustomerName = item.Customer.FirstName + ' ' + item.Customer.LastName,
                    ReserveStatus = status,
                    Id = item.Id
                });
            }

            return VMs;
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

            //return rooms;

            // Filter the checkin time for customer 
            var verbalRoomRent = context.VerbalRoomRents.Where(c => c.CheckIn < fromDate && c.CheckOut > toDate).ToList();

            foreach (var item in verbalRoomRent)
            {
                foreach (var roomRent in item.Rooms)
                {
                    rooms.Remove(roomRent); 
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
