using Microsoft.AspNetCore.Http;
using PaniMusic.Services.Map.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.MusicVideo.Add
{
    public class AddMusicVideoInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام موزیک ویدیو الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "کاور")]
        [UploadFileSize(10048576)]
        [Required(ErrorMessage = "انتخاب تصویر برای موزیک ویدیو الزامی است.")]
        public IFormFile MyCoverImage { get; set; }

        [Display(Name = "کیفیت 480")]
        [UploadFileSize(70048576)]
        public IFormFile MyQuality480 { get; set; }

        [Display(Name = "کیفیت 720")]
        [UploadFileSize(150048576)]
        public IFormFile MyQuality720 { get; set; }

        [Display(Name = "کیفیت 1080")]
        [UploadFileSize(200048576)]
        public IFormFile MyQuality1080 { get; set; }

        [Display(Name = "متن موزیک")]
        public string Lyric { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "موزیک ویدیو باید حاوی لینک باشد.")]
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

        [Display(Name = "سبک")]
        [Required(ErrorMessage = "سبک موزیک ویدیو باید مشخص گردد.")]
        public int StyleId { get; set; }

        [Display(Name = "خواننده")]
        [Required(ErrorMessage = "خواننده موزیک ویدیو باید مشخص گردد.")]
        public int ArtistId { get; set; }
    }
}
