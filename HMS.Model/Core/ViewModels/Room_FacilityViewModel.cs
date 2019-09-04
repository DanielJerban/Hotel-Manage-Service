using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Model.Core.ViewModels
{
    public class Room_FacilityViewModel
    {
        // Room Id
        public Guid Id { get; set; }

        [DisplayName("شماره اتاق")]
        [Required(ErrorMessage = "این فیلد الزامیست.")]
        public string RoomNumber { get; set; }

        [DisplayName("امتیاز"), Range(0, 5, ErrorMessage = "امتیاز باید ما بین 0 و 5 باشد")]
        public int? Rate { get; set; }

        [DisplayName("توضیحات بیشتر")]
        public string Description { get; set; }

        [DisplayName("تعداد تخت خواب یک نفره")]
        [Required(ErrorMessage = "این فیلد الزامیست.")]
        public byte SingleBedCount { get; set; }

        [DisplayName("تعداد تخت خواب دو نفره")]
        [Required(ErrorMessage = "این فیلد الزامیست.")]
        public byte DoubleBedCount { get; set; }

        [DisplayName("سرگرمی")]
        public string Entertainment { get; set; }
    }
}
