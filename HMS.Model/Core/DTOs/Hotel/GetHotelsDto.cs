using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Model.Core.DTOs.Hotel
{
    public class GetHotelsDto
    {
        public Guid Id { get; set; }

        [DisplayName("نام"), Required]
        public string Name { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Range(0, 5), DisplayName("امتیاز")]
        public int? Rate { get; set; }

        public int? RoomsCount { get; set; }
    }
}