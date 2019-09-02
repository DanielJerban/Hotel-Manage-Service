using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class CreateFelowCustomerDto
    {
        public Guid CustomerId { get; set; }
        public List<DomainModels.Customer> CustomerChildren { get; set; }
    }
}
