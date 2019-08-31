using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Customer
{
    public class CreateCustomerDto
    {
        // Customer fields 
        public Guid Id { get; set; }

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

        // Account Fields 
        [Required(ErrorMessage = "فیلد ایمیل الزامیست")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "فیلد رمز عبور الزامیست")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل دارای 6 کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن همخوانی ندارند.")]
        public string ConfirmPassword { get; set; }
    }
}
