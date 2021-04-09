using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.AlbumTrack.Add
{
    public class AddAlbumTrackInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام آهنگ الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "کیفیت 128")]
        public IFormFile MyQuality128 { get; set; }

        [Display(Name = "کیفیت 320")]
        public IFormFile MyQuality320 { get; set; }

        [Display(Name = "سبک")]
        [Required(ErrorMessage = "سبک آهنگ باید مشخص گردد.")]
        public int StyleId { get; set; }

        [Display(Name = "خواننده")]
        [Required(ErrorMessage = "خواننده آهنگ باید مشخص گردد.")]
        public int ArtistId { get; set; }

        [Display(Name = "آلبوم")]
        [Required(ErrorMessage = "پر کردن فیلد آلبوم الزامی است.")]
        public int? AlbumId { get; set; }
    }
}
