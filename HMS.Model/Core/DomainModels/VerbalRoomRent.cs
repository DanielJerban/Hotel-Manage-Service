using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class VerbalRoomRent : ObjectModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime AbsoluteCheckOut { get; set; }
        public List<Customer> ChildCustomer { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
