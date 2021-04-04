using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.GalleryImage.Add
{
    public class AddGalleryImageInput
    {
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "انتخاب تصویر الزامی است.")]
        public IFormFile MyImage { get; set; }

        [Display(Name = "شناسه دسته بندی")]
        [Required(ErrorMessage = "انتخاب دسته بندی الزامی است.")]
        public int GalleryCategoryId { get; set; }
    }
}
