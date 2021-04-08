using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserCrud userCrud;

        public DeleteUserModel(IUserCrud userCrud)
        {
            this.userCrud = userCrud;
        }

        public IdentityResult Result { get; set; }

        [TempData]
        public bool DeleteUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Result = await userCrud.DeleteUser(id);

            DeleteUser = Result.Succeeded;

            return RedirectToPage("AllUsers");
        }
    }
}
