using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using PaniMusic.Services.Map.CrudDtos.User.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewUserModel : PageModel
    {
        private readonly IUserCrud userCrud;

        public NewUserModel(IUserCrud userCrud)
        {
            this.userCrud = userCrud;
        }

        [BindProperty]
        public AddUserInput Input { get; set; }

        public IdentityResult Result { get; set; }

        [TempData]
        public bool AddUser { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Result = await userCrud.AddUser(Input);

            foreach (var error in Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);

                return Page();
            }

            AddUser = Result.Succeeded;

            return RedirectToPage("AllUsers");
        }
    }
}
