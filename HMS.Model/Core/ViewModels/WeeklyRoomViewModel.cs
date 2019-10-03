using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.ViewModels
{
    public class WeeklyRoomViewModel
    {
        // Room Id
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public Status RoomStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid? CheckingId { get; set; }
        public Guid? ReserveId { get; set; }
    }
}
