using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Core.Interfaces
{
    public interface IReservationRepo : IRepository<Reservation>
    {
        /// <summary>
        /// Filters all free rooms between a time 
        /// </summary>
        List<Room> GetEmptyRooms(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Returns all free rooms without any filtering
        /// </summary>
        List<Room> GetAllFreeRooms();

        /// <summary>
        /// Returns the name of the parent customer of a reservation
        /// </summary>
        string GetReservationCustomerName(Guid Id);

        /// <summary>
        /// Returns the number of a reservation's rooms that has been reserved by a customer 
        /// </summary>
        List<string> GetReservationCustomerRooms(Guid Id);

        /// <summary>
        /// Get All Reservation
        /// </summary>
        List<ReservationViewModel> GetAllReservation();
    }
}
