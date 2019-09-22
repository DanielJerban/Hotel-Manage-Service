using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Checking;

namespace HMS.Service.Core.Interfaces
{
    public interface ICheckingRepo : IRepository<Checking>
    {
        List<CheckingDto> GetCheckings();
    }
}
