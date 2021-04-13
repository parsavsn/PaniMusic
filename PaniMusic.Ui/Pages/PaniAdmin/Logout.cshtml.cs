using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    [Authorize(Policy = "AdminPanel")]
    [ValidateAntiForgeryToken]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
