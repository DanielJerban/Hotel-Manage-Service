using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class GetVerbalRentViewModel
    {
        public Guid Id { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string AbsoluteCheckOut { get; set; }
        public string ParentCustomerName { get; set; }
        public List<string> RoomsNumber { get; set; }
    }
}
