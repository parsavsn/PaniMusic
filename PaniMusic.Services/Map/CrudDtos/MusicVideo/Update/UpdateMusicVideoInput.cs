using Microsoft.AspNetCore.Http;
using PaniMusic.Services.Map.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.MusicVideo.Update
{
    public class UpdateMusicVideoInput
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "وارد کردن شناسه الزامی است.")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "کاور")]
        [UploadFileSize(10048576)]
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
        public string Link { get; set; }

        [Display(Name = "تگ title")]
        public string TitleTag { get; set; }

        [Display(Name = "تگ metadescription")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ metatag")]
        public string MetaTag { get; set; }

        [Display(Name = "سبک")]
        public int StyleId { get; set; }

        [Display(Name = "خواننده")]
        public int ArtistId { get; set; }
    }
}
