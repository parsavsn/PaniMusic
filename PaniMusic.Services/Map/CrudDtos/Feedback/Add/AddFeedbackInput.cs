using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.Feedback.Add
{
    public class AddFeedbackInput
    {
        [Display(Name = "نام نمایشی در نظرات")]
        [Required(ErrorMessage = "وارد نمودن فیلد نام الزامی است.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام نمایشی باید حداقل دارای 3 کاراکتر و حداکثر دارای 50 کاراکتر باشد.")]
        public string Name { get; set; }

        [Display(Name = "نظر شما")]
        [Required(ErrorMessage = "وارد نمودن فید نظر الزامی است.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "نظر شما باید حداقل دارای 1 کاراکتر و حداکثر دارای 500 کاراکتر باشد.")]
        public string Comment { get; set; }

        [Display(Name = "امتیاز")]
        [Range(1, 5, ErrorMessage = "امتیاز شما باید بین 1 تا 5 سال باشد.")]
        [Required(ErrorMessage = "امتیازدهی الزامی است.")]
        public int Rate { get; set; }

        public bool IsAccept { get; set; } = false;

        public int? TrackId { get; set; }

        public int? AlbumId { get; set; }

        public int? MusicVideoId { get; set; }

        [Required(ErrorMessage = "وارد کردن شناسه کاربری الزامی است.")]
        public string UserId { get; set; }
    }
}
