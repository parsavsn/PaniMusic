using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Artist.Update
{
    public class UpdateArtistInput
    {
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile MyImage { get; set; }

        [Display(Name = "بیوگرافی")]
        public string BioGraphy { get; set; }

        [Display(Name = "لینک")]
        public string Link { get; set; }

        [Display(Name = "تگ title")]
        public string TitleTag { get; set; }

        [Display(Name = "تگ metadescription")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ metatag")]
        public string MetaTag { get; set; }
    }
}
