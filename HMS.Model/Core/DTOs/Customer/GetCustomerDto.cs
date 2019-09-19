using System;
using System.ComponentModel;

namespace HMS.Model.Core.DTOs.Customer
{
    public class GetCustomerDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [DisplayName("نام")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        public string NationalNo { get; set; }

        [DisplayName("شماره پاسپورت")]
        public string PassportNo { get; set; }
    }
}
