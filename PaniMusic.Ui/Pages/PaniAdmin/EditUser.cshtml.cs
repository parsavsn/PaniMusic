using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using PaniMusic.Services.Map.CrudDtos.User.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class EditUserModel : PageModel
    {
        private readonly IUserCrud userCrud;

        public EditUserModel(IUserCrud userCrud)
        {
            this.userCrud = userCrud;
        }

        [BindProperty]
        public UpdateUserInput Input { get; set; }

        public User User { get; set; }

        public IdentityResult Result { get; set; }

        [TempData]
        public bool EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            User = await userCrud.GetUserById(id);

            if (User == null)
                return RedirectToPage("AllUsers");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(UpdateUserInput input)
        {
            if (!ModelState.IsValid)
                return Page();

            Result = await userCrud.UpdateUser(input);

            foreach (var error in Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);

                return Page();
            }

            EditUser = Result.Succeeded;

            return RedirectToPage("AllUsers");
        }
    }
}
