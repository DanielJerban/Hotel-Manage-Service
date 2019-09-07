using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public class Reservation : ObjectModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        public Customer Customer { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
