using System;
using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;

namespace HMS.Service.Core.Interfaces
{
    public interface IRoomRepo: IRepository<Room>
    {
        List<Room_FacilityViewModel> GetRooms();

        /// <summary>
        /// Return all free rooms in a range of time 
        /// </summary>
        List<Room> AllFreeRoomsInDateRange(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets all free rooms depend on reserve and checking
        /// </summary>
        List<Room> GetAllFreeRooms();
    }
}