using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public Guid customerId { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "فیلد نام الزامیست.")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "فیلد نام خانوادگی الزامیست.")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        [Required(ErrorMessage = "فیلد کد ملی الزامیست.")]
        public string NationalNo { get; set; }

        [DisplayName("شماره پاسپورت")]
        [Required(ErrorMessage = "فیلد پاسپورت الزامیست.")]
        public string PassportNo { get; set; }
    }
}
