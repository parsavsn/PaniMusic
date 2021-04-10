using Microsoft.AspNetCore.Http;
using PaniMusic.Services.Map.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.AlbumTrack.Update
{
    public class UpdateAlbumTrackInput
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "پر کردن شناسه الزامی است.")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام آهنگ الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "کیفیت 128")]
        [UploadFileSize(10048576)]
        public IFormFile MyQuality128 { get; set; }

        [Display(Name = "کیفیت 320")]
        [UploadFileSize(20048576)]
        public IFormFile MyQuality320 { get; set; }
    }
}
