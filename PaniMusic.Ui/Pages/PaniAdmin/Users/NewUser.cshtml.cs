using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.User.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin.Users
{
    [Authorize(Policy = "AdminPanel")]
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
            if (!User.HasClaim(PaniClaims.NewItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            if (Input.UserPanel == true && Input.AdminPanel == true)
            {
                ModelState.AddModelError("", "امکان انتخاب همزمان پنل ادمین و پنل کاربری وجود ندارد");

                return Page();
            }

            if ((Input.UserPanel == true) && (Input.NewItem == true || Input.EditItem == true || Input.DeleteItem == true))
            {
                ModelState.AddModelError("", "با انتخاب پنل کاربری ، امکان انتخاب سایر موارد را نخواهید داشت");

                return Page();
            }

            if ((Input.NewItem == true || Input.EditItem == true || Input.DeleteItem == true) && (Input.AdminPanel == false))
            {
                ModelState.AddModelError("", "برای دسترسی به ایجاد ، ویرایش یا حذف آیتم ، باید پنل ادمین را نیز انتخاب کنید");

                return Page();
            }

            if (Input.UserPanel == false && Input.AdminPanel == false && Input.NewItem == false && Input.EditItem == false && Input.NewItem == false)
            {
                ModelState.AddModelError("", "کاربر وارد شده باید حداقل یکی از دسترسی ها را داشته باشد");

                return Page();
            }

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
