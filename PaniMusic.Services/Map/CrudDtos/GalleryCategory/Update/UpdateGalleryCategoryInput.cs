using Microsoft.AspNetCore.Http;
using PaniMusic.Services.Map.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.GalleryCategory.Update
{
    public class UpdateGalleryCategoryInput
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "وارد کردن شناسه الزامی است.")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام دسته بندی الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        [UploadFileSize(10048576)]
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

        [Display(Name = "تگ metakeywoard")]
        [Required(ErrorMessage = "پر کردن تگ metakeywoard الزامی است.")]
        public string MetaTag { get; set; }
    }
}
