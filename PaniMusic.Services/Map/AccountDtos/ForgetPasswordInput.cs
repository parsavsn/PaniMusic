using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.AccountDtos
{
    public class ForgetPasswordInput
    {
        [Required(ErrorMessage = "وارد کردن فیلد ایمیل ضروری است.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد شود.")]
        public string Email { get; set; }
    }
}
