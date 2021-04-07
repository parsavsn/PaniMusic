using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.CrudDtos.User.Update
{
    public class UpdateUserInput
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "وارد کردن شناسه الزامی است.")]
        public string Id { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "رمز عبور باید حداقل داری 8 کاراکتر که شامل  حداقل یک عدد و یک حرف است باشد.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "تایید ایمیل")]
        public bool EmailConfirmed { get; set; }
    }
}
