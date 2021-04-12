using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.AccountDtos
{
    public class LoginInput
    {
        [Required(ErrorMessage = "پر کردن فیلد ایمیل الزامی است.")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد شود.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد رمز عبور الزامی است.")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
