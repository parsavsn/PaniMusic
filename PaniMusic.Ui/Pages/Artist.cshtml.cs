using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;

namespace PaniMusic.Ui.Pages
{
    public class ArtistModel : PageModel
    {
        private readonly IArtistCrud artistCrud;

        public ArtistModel(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }

        public Artist Artist { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            Artist = await artistCrud.GetArtistByLink(link);

            if (Artist == null)
                return RedirectToPage("AllArtists");

            return Page();
        }
    }
}
