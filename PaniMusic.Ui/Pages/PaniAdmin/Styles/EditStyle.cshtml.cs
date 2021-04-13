using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.Style.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Styles
{
    [Authorize(Policy = "AdminPanel")]
    public class EditStyleModel : PageModel
    {
        private readonly IStyleCrud styleCrud;

        public EditStyleModel(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        [BindProperty]
        public UpdateStyleInput Input { get; set; }

        public Style Style { get; set; }

        [TempData]
        public bool EditStyle { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Style = await styleCrud.GetStyleById(id);

            if (Style == null)
                return RedirectToPage("AllStyles");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            EditStyle = await styleCrud.UpdateStyle(Input);

            return RedirectToPage("AllStyles");
        }
    }
}
