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
    public class UpdatePasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public UpdatePasswordModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;
        }

        [BindProperty]
        public UpdatePasswordInput Input { get; set; }

        public bool? Result { get; set;}

        public IActionResult OnGetAsync([FromQuery]string userName, [FromQuery]string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            ViewData["UserName"] = userName;

            ViewData["Token"] = token;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Input.UserName);

                if (user == null)
                {
                    return RedirectToPage("ForgetPassword");
                }

                var result = await userManager.ResetPasswordAsync(user, Input.Token, Input.NewPassword);

                if (result.Succeeded)
                {
                    Result = result.Succeeded;

                    return Page();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}
