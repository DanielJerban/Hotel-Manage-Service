using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class Reserve_Room : ObjectModel
    {
        public Guid ReserveId { get; set; }
        public Reserve Reserve { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
