using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> RoomsNumber { get; set; }
        public string CustomerName { get; set; }
        public string ReserveStatus { get; set; }
    }
}
