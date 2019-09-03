using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Hotel
{
    public class CreateHotelDto
    {
        [DisplayName("نام"), Required]
        public string Name { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Range(0, 5), DisplayName("امتیاز")]
        public int? Rate { get; set; }
    }
}
