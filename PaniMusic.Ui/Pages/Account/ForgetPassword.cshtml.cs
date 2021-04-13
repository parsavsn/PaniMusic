using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Account;
using PaniMusic.Services.Map.AccountDtos;

namespace PaniMusic.Ui.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class ForgetPasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;

        private readonly IEmailSender account;

        public ForgetPasswordModel(UserManager<User> userManager, IEmailSender account)
        {
            this.userManager = userManager;

            this.account = account;
        }

        [BindProperty]
        public ForgetPasswordInput Input { get; set; }

        public bool? Result { get; set; }

        public bool? NotAccepted { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Input.Email);

                if (user == null)
                {
                    Result = true;

                    return Page();
                }

                if (user.EmailConfirmed == false)
                {
                    NotAccepted = true;

                    return Page();
                }

                var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

                var emailMessage =
                        Url.Page("UpdatePassword",
                        values: new { username = user.Email, token = resetPasswordToken },
                        pageHandler: null,
                        protocol: Request.Scheme
                        );

                await account.SendEmail(Input.Email, "بروز رسانی رمز عبور در پانی موزیک", emailMessage);

                Result = true;

                return Page();
            }

            return Page();
        }
    }
}
