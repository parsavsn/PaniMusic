using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.Style.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin.Styles
{
    [Authorize(Policy = "AdminPanel")]
    public class NewStyleModel : PageModel
    {
        private readonly IStyleCrud styleCrud;

        public NewStyleModel(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        [BindProperty]
        public AddStyleInput Input { get; set; }

        [TempData]
        public bool AddStyle { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.NewItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            AddStyle = await styleCrud.AddStyle(Input);

            return RedirectToPage("AllStyles");
        }
    }
}
