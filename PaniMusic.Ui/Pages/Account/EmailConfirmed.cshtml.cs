using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;

namespace PaniMusic.Ui.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class EmailConfirmedModel : PageModel
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public EmailConfirmedModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            var user = await userManager.FindByNameAsync(userName);

            if (user == null || user.EmailConfirmed == true)
                return RedirectToPage("/Index");

            await userManager.ConfirmEmailAsync(user, token);

            await signInManager.SignInAsync(user, true);

            return Page();
        }
    }
}
