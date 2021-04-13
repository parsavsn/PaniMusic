using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.Artist.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Artists
{
    [Authorize(Policy = "AdminPanel")]
    public class EditArtistModel : PageModel
    {
        private readonly IArtistCrud artistCrud;

        public EditArtistModel(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public UpdateArtistInput Input { get; set; }

        public Artist Artist { get; set; }

        [TempData]
        public bool EditArtist { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Artist = await artistCrud.GetArtistById(id);

            if (Artist == null)
                return RedirectToPage("AllArtists");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            EditArtist = await artistCrud.UpdateArtist(Input);

            return RedirectToPage("AllArtists");
        }
    }
}
