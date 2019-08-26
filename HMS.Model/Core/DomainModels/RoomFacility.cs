using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public enum BedModel
    {
        Single,Double
    }

    public class RoomFacility : ObjectModel
    {
        [DisplayName("نوع تخت خواب")]
        public BedModel BedModel { get; set; }

        [DisplayName("تعداد تخت خواب")]
        public byte bedCount { get; set; }

        [DisplayName("سرگرمی")]
        public string Entertainment { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
