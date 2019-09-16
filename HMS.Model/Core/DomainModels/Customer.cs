using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HMS.Model.Core.DomainModels
{
    public class Customer : Person
    {
        [DisplayName("شماره پاسپورت")]
        public string PassportNo { get; set; }

        public HotelData Hotel { get; set; }

        [DisplayName("مسافر همراه")]
        public Guid? ParentId { get; set; }

        public ICollection<Passenger> Passengers { get; set; }
    }
}
