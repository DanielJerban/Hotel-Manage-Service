using System;
using System.ComponentModel;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.DTOs.ContactInfo
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }

        [DisplayName("شماره تلفن")]
        public string TelNo { get; set; }

        [DisplayName("نوع تلفن")]
        public TelType TelType { get; set; }

        [DisplayName("آدرس")]
        public string Address { get; set; }
    }
}
