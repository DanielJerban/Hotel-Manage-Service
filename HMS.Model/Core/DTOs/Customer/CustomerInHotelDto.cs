using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class CustomerInHotelDto
    {
        public Guid PassengerId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalNo { get; set; }

        public string PassportNo { get; set; }
    }
}
