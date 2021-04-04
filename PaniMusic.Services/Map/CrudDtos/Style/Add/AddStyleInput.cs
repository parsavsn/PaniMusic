using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Style.Add
{
    public class AddStyleInput
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن نام سبک الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "درباره سبک")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامی است.")]
        public string Description { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "سبک باید حاوی لینک باشد.")]
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
