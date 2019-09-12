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
        List<Room> GetEmptyRoom(DateTime fromDate, DateTime toDate);
        List<Room> GetAllFreeRooms();
        string GetReservationCustomerName(Guid Id);
        List<string> GetReservationCustomerRooms(Guid Id);

        /// <summary>
        /// Get All Reservation
        /// </summary>
        List<ReservationViewModel> GetAllReservation();
    }
}
