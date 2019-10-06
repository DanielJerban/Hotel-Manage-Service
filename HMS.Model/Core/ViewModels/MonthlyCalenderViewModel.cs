using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class MonthlyCalenderViewModel
    {
        public List<string> MonthPersianDates { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
    }
}
