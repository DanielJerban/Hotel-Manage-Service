using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Reserve
{
    public class CreateReserveApiDto
    {
        public List<string> RoomsId { get; set; }
        public List<string> FellowsId { get; set; }
        public string HostId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
