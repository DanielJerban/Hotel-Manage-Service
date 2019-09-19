using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;

namespace HMS.Service.Core.Interfaces
{
    public interface IReserveRepo : IRepository<Reserve>
    {
        /// <summary>
        /// Get all reserves include customer
        /// </summary>
        List<Reserve> GetAllReserves();

        /// <summary>
        /// Get the reserved rooms for a specific reserve
        /// </summary>
        /// <param name="Id">The Id of the Reserve</param>
        List<Room> GetReservedRooms(Guid Id);
    }
}
