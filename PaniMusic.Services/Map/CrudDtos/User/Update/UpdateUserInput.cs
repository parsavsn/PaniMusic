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

        [Required(ErrorMessage = "پر کردن فید نام و نام خانوادگی الزامی است.")]
        [Display(Name = "نام و نام خانوادگی")]
        public string Name { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد ایمیل الزامی است.")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد شود.")]
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

        [Display(Name = "پنل کاربری")]
        public bool UserPanel { get; set; }

        [Display(Name = "پنل ادمین")]
        public bool AdminPanel { get; set; }

        [Display(Name = "ایجاد آیتم")]
        public bool NewItem { get; set; }

        [Display(Name = "ویرایش آیتم")]
        public bool EditItem { get; set; }

        [Display(Name = "حذف آیتم")]
        public bool DeleteItem { get; set; }
    }
}
