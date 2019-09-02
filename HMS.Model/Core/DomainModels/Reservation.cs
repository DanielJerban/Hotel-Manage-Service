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

        // Each reserve has n rooms 
        // and 
        // Each room belong to n reserve 
        // I will handle the overlap later in the code 
        public List<Reservation_Room> Rooms { get; set; }
    }
}
