using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class AllStylesModel : PageModel
    {
        private readonly IStyleCrud styleCrud;

        public AllStylesModel(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        public List<Style> AllStyles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AllStyles = await styleCrud.GetAllStyles();

            return Page();
        }
    }
}
