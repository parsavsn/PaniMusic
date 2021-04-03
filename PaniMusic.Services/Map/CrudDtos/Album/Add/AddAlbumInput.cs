using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Album.Add
{
    public class AddAlbumInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام آلبوم الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "کاور")]
        [Required(ErrorMessage = "انتخاب تصویر برای آلبوم الزامی است.")]
        public IFormFile MyCoverImage { get; set; }

        [Display(Name = "کیفیت 128")]
        public IFormFile MyQuality128 { get; set; }

        [Display(Name = "کیفیت 320")]
        public IFormFile MyQuality320 { get; set; }


        [Display(Name = "لینک")]
        [Required(ErrorMessage = "آلبوم باید حاوی لینک باشد.")]
        public string Link { get; set; }

        [Display(Name = "تگ title")]
        [Required(ErrorMessage = "پر کردن تگ title الزامی است.")]
        public string TitleTag { get; set; }

        [Display(Name = "تگ metadescription")]
        [Required(ErrorMessage = "پر کردن تگ metadescription الزامی است.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ metatag")]
        [Required(ErrorMessage = "پر کردن تگ metatag الزامی است.")]
        public string MetaTag { get; set; }

        [Display(Name = "سبک")]
        [Required(ErrorMessage = "سبک آلبوم باید مشخص گردد.")]
        public int StyleId { get; set; }

        [Display(Name = "خواننده")]
        [Required(ErrorMessage = "خواننده آلبوم باید مشخص گردد.")]
        public int ArtistId { get; set; }
    }
}
