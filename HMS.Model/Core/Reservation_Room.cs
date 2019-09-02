using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core
{
    public class Reservation_Room : ObjectModel
    {
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
    }
}
