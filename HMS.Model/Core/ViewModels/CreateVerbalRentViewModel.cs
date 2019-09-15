using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class CreateVerbalRentViewModel
    {
        public string ParentId { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public List<string> Rooms { get; set; }
        public List<string> FelowCustomer { get; set; }
    }
}
