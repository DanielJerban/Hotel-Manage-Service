using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;

namespace HMS.Service.Core.Interfaces
{
    public interface IRoomRepo: IRepository<Room>
    {
        List<Room_FacilityViewModel> GetRooms();
    }
}