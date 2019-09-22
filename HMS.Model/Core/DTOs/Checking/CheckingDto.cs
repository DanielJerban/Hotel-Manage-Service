using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Checking
{
    public class CheckingDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string CustomerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RoomNumber { get; set; }
    }
}
