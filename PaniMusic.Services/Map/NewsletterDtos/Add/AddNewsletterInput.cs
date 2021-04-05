using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.NewsletterDtos.Add
{
    public class AddNewsletterInput
    {
        [Display(Name = "نام")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام وارد شده باید حداقل دارای 3 کاراکتر و حداکثر دارای 50 کاراکتر باشد.")]
        [Required(ErrorMessage = "پر کردن فیلد نام الزامی است.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام وارد شده باید حداقل دارای 3 کاراکتر و حداکثر دارای 50 کاراکتر باشد.")]
        [Required(ErrorMessage = "پر کردن فیلد نام خانوادگی الزامی است.")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "پر کردن فیلد ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است.")]
        public string Email { get; set; }
    }
}
