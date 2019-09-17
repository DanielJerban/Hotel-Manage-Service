using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class Checking : ObjectModel
    {
        [Index("U!_ReservationNumber", IsUnique = true)]
        public long Number { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public Guid? ReserveId { get; set; }
        public virtual Reserve Reserve { get; set; }

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
