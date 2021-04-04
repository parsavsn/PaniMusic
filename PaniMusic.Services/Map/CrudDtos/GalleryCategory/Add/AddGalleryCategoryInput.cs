using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add
{
    public class AddGalleryCategoryInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام دسته بندی الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "انتخاب تصویر برای دسته بندی الزامی است.")]
        public IFormFile MyImage { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "دسته بندی باید حاوی لینک باشد.")]
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
