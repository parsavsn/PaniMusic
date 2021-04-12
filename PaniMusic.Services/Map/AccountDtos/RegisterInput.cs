using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.AccountDtos
{
    public class RegisterInput
    {
        [Required(ErrorMessage = "پر کردن فید نام و نام خانوادگی الزامی است.")]
        [Display(Name = "نام و نام خانوادگی")]
        public string Name { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد ایمیل الزامی است.")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد شود.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد رمز عبور الزامی است.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "رمز عبور باید حداقل داری 8 کاراکتر که شامل  حداقل یک عدد و یک حرف است باشد.")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد تکرار رمز عبور الزامی است.")]
        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
