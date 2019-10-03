using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class CustomerInHotelDetailDto
    {
        public string HostName { get; set; }
        public Guid? CustomerId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string RoomsNumber { get; set; }
    }
}
