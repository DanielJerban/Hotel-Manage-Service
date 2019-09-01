using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.DTOs.ContactInfo
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }

        [DisplayName("شماره تلفن")]
        [Required(ErrorMessage = "شماره تلفن الزامیست.")]
        public string TelNo { get; set; }

        [DisplayName("نوع تلفن")]
        public string TelType { get; set; }

        [DisplayName("آدرس")]
        public string Address { get; set; }
    }
}
