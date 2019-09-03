using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public class HotelData : ObjectModel
    {
        [DisplayName("نام"), Required]
        public string Name { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Range(0, 5), DisplayName("امتیاز")]
        public int? Rate { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
