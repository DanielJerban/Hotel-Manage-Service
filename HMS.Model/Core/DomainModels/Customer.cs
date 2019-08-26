using System.ComponentModel;

namespace HMS.Model.Core.DomainModels
{
    public class Customer : Person
    {
        [DisplayName("شماره پاسپورت")]
        public string PassportNo { get; set; }

        public HotelData Hotel { get; set; }
    }
}
