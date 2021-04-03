using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Album.Update
{
    public class UpdateAlbumInput
    {
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "کاور")]
        public IFormFile MyCoverImage { get; set; }

        [Display(Name = "کیفیت 128")]
        public IFormFile MyQuality128 { get; set; }

        [Display(Name = "کیفیت 320")]
        public IFormFile MyQuality320 { get; set; }

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
