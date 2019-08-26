using System.Collections.Generic;
using System.ComponentModel;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public class Person : ObjectModel
    {
        [DisplayName("نام")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        public string NationalNo { get; set; }

        public ApplicationUser User { get; set; }

        public List<ContactInfo> Infos { get; set; }
    }
}