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

        /// <summary>
        /// Returns all free rooms without any filtering
        /// </summary>
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

        /// <summary>
        /// Filters all free rooms in a time 
        /// </summary>
        public List<Room> GetEmptyRooms(DateTime fromDate, DateTime toDate)
        {
            // All Rooms 
            var rooms = context.Rooms.Include(c => c.Facility).ToList();

            // All reservations in a specific time 
            var reservations = context.Reservations
                .Include(c => c.Rooms)
                .Where(c =>
                c.FromDate >= fromDate && c.ToDate >= toDate || /* 1 */
                c.FromDate <= fromDate && c.ToDate <= toDate || /* 2 */
                c.FromDate <= fromDate && c.ToDate >= toDate || /* 3 */
                c.FromDate >= fromDate && c.ToDate <= toDate    /* 4 */
                )
                .ToList();


            // Filter by reserved Rooms 
            foreach (var reserv in reservations)
            {
                foreach (var reserveredRoom in reserv.Rooms)
                {
                    // Remove the full rooms from all rooms 
                    rooms.Remove(reserveredRoom);
                }
            }

            var rents = context.VerbalRoomRents
                .Include(c => c.Rooms)
                .Where(c =>
                c.CheckIn >= fromDate && c.CheckOut >= toDate || /* 1 */
                c.CheckIn <= fromDate && c.CheckOut <= toDate || /* 2 */
                c.CheckIn <= fromDate && c.CheckOut >= toDate || /* 3 */
                c.CheckIn >= fromDate && c.CheckOut <= toDate    /* 4 */
                )
                .ToList();

            // Filter by Verbal Room Rent 
            foreach (var rent in rents)
            {
                foreach (var rentedRoom in rent.Rooms)
                {
                    // Remove the full rooms from all rooms 
                    rooms.Remove(rentedRoom);
                }
            }

            // Return All Left Rooms
            return rooms;
        }

        /// <summary>
        /// Returns the name of the parent customer of a reservation
        /// </summary>
        public string GetReservationCustomerName(Guid Id)
        {
            var customerReserve = context.Reservations.Include(c => c.Customer).FirstOrDefault(c => c.Id == Id);

            return customerReserve.Customer.FirstName + ' ' + customerReserve.Customer.LastName;
        }

        /// <summary>
        /// Returns the number of a reservation's rooms that has been reserved by a customer 
        /// </summary>
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
