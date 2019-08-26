using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class Employee : Person
    {
        [DisplayName("شماره کارمند"), Required]
        public long EmployeeNumber { get; set; }

        [DisplayName("وطیفه"), Required]
        public string Duty { get; set; }

        public virtual HotelData Hotel { get; set; }
    }
}
