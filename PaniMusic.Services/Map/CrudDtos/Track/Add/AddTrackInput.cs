using Microsoft.AspNetCore.Http;
using PaniMusic.Services.Map.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Track.Add
{
    public class AddTrackInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام آهنگ الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "کاور")]
        [UploadFileSize(10048576)]
        [Required(ErrorMessage = "انتخاب تصویر برای آهنگ الزامی است.")]
        public IFormFile MyCoverImage { get; set; }

        [Display(Name = "کیفیت 128")]
        [UploadFileSize(10048576)]
        public IFormFile MyQuality128 { get; set; }

        [Display(Name = "کیفیت 320")]
        [UploadFileSize(15048576)]
        public IFormFile MyQuality320 { get; set; }

        [Display(Name = "متن آهنگ")]
        public string Lyric { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "آهنگ باید حاوی لینک باشد.")]
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
        [Required(ErrorMessage = "سبک آهنگ باید مشخص گردد.")]
        public int StyleId { get; set; }

        [Display(Name = "خواننده")]
        [Required(ErrorMessage = "خواننده آهنگ باید مشخص گردد.")]
        public int ArtistId { get; set; }

        [Display(Name = "آلبوم")]
        public int? AlbumId { get; set; }
    }
}
