using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.User.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Users
{
    [Authorize(Policy = "AdminPanel")]
    public class EditUserModel : PageModel
    {
        private readonly IUserCrud userCrud;

        private readonly UserManager<User> userManager;

        public EditUserModel(IUserCrud userCrud, UserManager<User> userManager)
        {
            this.userCrud = userCrud;

            this.userManager = userManager;
        }

        [BindProperty]
        public UpdateUserInput Input { get; set; }

        public User MyUser { get; set; }

        public IdentityResult Result { get; set; }

        public List<string> HasClaims { get; set; }

        public List<string> HasNotClaims { get; set; }

        [TempData]
        public bool EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            MyUser = await userCrud.GetUserById(id);

            if (MyUser == null)
                return RedirectToPage("AllUsers");

            var allClaims = AllPaniClaims.AllClaims;

            var userClaims = await userManager.GetClaimsAsync(MyUser);

            HasClaims = userClaims.Select(x => x.Type).ToList();

            HasNotClaims = allClaims.Where(x => userClaims.All(y => y.Type != x.Type))
                .Select(y => y.Type)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            MyUser = await userCrud.GetUserById(Input.Id);

            var allClaims = AllPaniClaims.AllClaims;

            var userClaims = await userManager.GetClaimsAsync(MyUser);

            HasClaims = userClaims.Select(x => x.Type).ToList();

            HasNotClaims = allClaims.Where(x => userClaims.All(y => y.Type != x.Type))
                .Select(y => y.Type)
                .ToList();

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

            Result = await userCrud.UpdateUser(Input);

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
