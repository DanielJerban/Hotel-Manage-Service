using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;

namespace HMS.Model.Core.ViewModels
{
    public class CreateCustomerViewModel
    {
        // Customer 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalNo { get; set; }
        public string PassportNo { get; set; }
        // ===============================================================================================


        // User
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
        // ===============================================================================================


        // Contact infos 
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
