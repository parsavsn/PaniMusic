using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteStyleModel : PageModel
    {
        private readonly IStyleCrud styleCrud;

        public DeleteStyleModel(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        [TempData]
        public bool DeleteStyle { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteStyle = await styleCrud.DeleteStyle(id);

            return RedirectToPage("AllStyles");
        }
    }
}
