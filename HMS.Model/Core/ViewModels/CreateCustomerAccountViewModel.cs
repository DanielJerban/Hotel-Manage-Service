using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.ViewModels
{
    public class CreateCustomerAccountViewModel
    {
        // Customer Fields
        [DisplayName("نام")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        public string NationalNo { get; set; }

        [DisplayName("شماره پاسپورت")]
        public string PassportNo { get; set; }


        // Contact Info Fields 
        [DisplayName("شماره تلفن")]
        public string TelNo { get; set; }

        //[DisplayName("نوع تلفن")]
        //public TelType TelType { get; set; }

        [DisplayName("آدرس")]
        public string Address { get; set; }


        // Account Fields 
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
