using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class DetailReserveViewModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> RoomNumbers { get; set; }
        public string CustomerName { get; set; }
        public string ReserveStatus { get; set; }
    }
}
