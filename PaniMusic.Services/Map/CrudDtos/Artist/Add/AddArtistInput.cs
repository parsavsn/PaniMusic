using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Artist.Add
{
    public class AddArtistInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام آلبوم الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "انتخاب تصویر برای هنرمند الزامی است.")]
        public IFormFile MyImage { get; set; }

        [Display(Name = "بیوگرافی")]
        [Required(ErrorMessage = "نوشتن بیوگرافی برای هنرمند الزامی است.")]
        public string BioGraphy { get; set; }

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
    }
}
