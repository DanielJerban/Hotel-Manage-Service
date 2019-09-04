using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public class RoomFacility : ObjectModel
    {
        [DisplayName("تعداد تخت خواب یک نفره")]
        public byte SingleBedCount { get; set; }

        [DisplayName("تعداد تخت خواب دو نفره")]
        public byte DoubleBedCount { get; set; }

        [DisplayName("سرگرمی")]
        public string Entertainment { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
