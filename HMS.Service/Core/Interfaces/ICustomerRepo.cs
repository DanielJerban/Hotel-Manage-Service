using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Customer;

namespace HMS.Service.Core.Interfaces
{
    public interface ICustomerRepo : IRepository<Customer>
    {
        List<GetCustomerDto> GetlFelowCustomers(Guid parentId);
    }
}
