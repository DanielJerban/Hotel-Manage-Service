using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.ViewModels
{
    public class CreateCustomerAccountViewModel
    {
        // Customer Fields
        [DisplayName("نام")]
        [Required(ErrorMessage = "فیلد نام الزامیست")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "فیلد نام خانوادگی الزامیست")]
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
        [Required(ErrorMessage = "فیلد ایمیل الزامیست")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "فیلد رمز عبور الزامیست")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل دارای 6 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن همخوانی ندارند")]
        public string ConfirmPassword { get; set; }
    }
}
