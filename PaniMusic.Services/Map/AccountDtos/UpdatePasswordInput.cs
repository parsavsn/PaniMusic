using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Services.Map.AccountDtos
{
    public class UpdatePasswordInput
    {
        [Required(ErrorMessage = "پر کردن فیلد رمز عبور جدید الزامی است.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "رمز عبور باید حداقل داری 8 کاراکتر که شامل  حداقل یک عدد و یک حرف است باشد.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد تکرار رمز عبور جدید الزامی است.")]
        [Compare(nameof(NewPassword), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
