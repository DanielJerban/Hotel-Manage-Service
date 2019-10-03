using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Room
{
    public class RoomCheckingDetailDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CheckingNumber { get; set; }
        public string HostName { get; set; }
        public List<string> Passengers { get; set; }
    }
}
