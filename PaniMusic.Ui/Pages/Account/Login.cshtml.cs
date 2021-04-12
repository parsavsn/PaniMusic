using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.Map.AccountDtos;

namespace PaniMusic.Ui.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public LoginInput Input { get; set; }

        public string ReturnUrl { get; set; }

        public string AccountLockOut { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (signInManager.IsSignedIn(User))
                return RedirectToPage("/Index");

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (signInManager.IsSignedIn(User))
                return RedirectToPage("/Index");

            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToPage("/Index");
                }

                if (result.IsLockedOut)
                {
                    AccountLockOut = "حساب کاربری شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";

                    return Page();
                }

                ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است");
            }

            return Page();
        }
    }
}
