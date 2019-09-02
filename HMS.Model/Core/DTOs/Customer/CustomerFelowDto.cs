using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class CustomerFelowDto
    {
        public Guid ParentCustomerId { get; set; }
        public List<DomainModels.Customer> FelowCustomer { get; set; }
        public CreateCustomerDto CreateCustomerDto { get; set; }
    }
}
