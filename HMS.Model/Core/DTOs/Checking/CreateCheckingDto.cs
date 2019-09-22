using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Checking
{
    public class CreateCheckingDto
    {
        public Guid Id { get; set; }
        public List<string> PassengersId { get; set; }
        public string HostId { get; set; }
        public string RoomId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
