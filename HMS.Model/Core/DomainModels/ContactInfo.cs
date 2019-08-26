using System.ComponentModel;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public enum TelType
    {
        Mobile,
        Home,
        Work
    }

    public class ContactInfo : ObjectModel
    {
        [DisplayName("شماره تلفن")]
        public string TelNo { get; set; }

        [DisplayName("نوع تلفن")]
        public TelType TelType { get; set; }

        [DisplayName("آدرس")]
        public string Address { get; set; }

        public Person Person { get; set; }
    }
}
