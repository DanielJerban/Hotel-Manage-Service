using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Room
{
    public class RoomReserveDetailDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ReserveNumber { get; set; }
        public string ReserveStatus { get; set; }
        public string HostName { get; set; }
        public List<string> FellowsName { get; set; }
    }
}
