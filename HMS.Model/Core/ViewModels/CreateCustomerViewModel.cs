using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.ContactInfo;

namespace HMS.Model.Core.ViewModels
{
    public class CreateCustomerViewModel
    {
        // Customer 
        [DisplayName("نام")]
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string NationalNo { get; set; }

        [DisplayName("شماره پاسپورت")]
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public string PassportNo { get; set; }
        // ===============================================================================================


        // User
        [Required(ErrorMessage = "فیلد ایمیل الزامیست")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "فیلد رمز عبور الزامیست")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل دارای 6 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور ")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن همخوانی ندارند")]
        public string ConfirmPassword { get; set; }
        // ===============================================================================================

        public ContactInfoDto contact { get; set; }
    }
}
