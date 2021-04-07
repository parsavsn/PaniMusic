using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteArtistModel : PageModel
    {
        private readonly IArtistCrud artistCrud;

        public DeleteArtistModel(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }

        [TempData]
        public bool DeleteArtist { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteArtist = await artistCrud.DeleteArtist(id);

            return RedirectToPage("AllArtists");
        }
    }
}
