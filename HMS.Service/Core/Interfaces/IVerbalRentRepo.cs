using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Core.Interfaces
{
    public interface IVerbalRentRepo : IRepository<VerbalRoomRent>
    {
        List<GetVerbalRentViewModel> GetAllRents();
    }
}
