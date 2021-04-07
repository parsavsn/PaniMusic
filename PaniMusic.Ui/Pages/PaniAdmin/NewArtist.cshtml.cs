using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewArtistModel : PageModel
    {
        private readonly IArtistCrud artistCrud;

        public NewArtistModel(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public AddArtistInput Input { get; set; }

        [TempData]
        public bool AddArtist { get; set; }

        public async Task<IActionResult> OnPostAsync(AddArtistInput input)
        {
            if (!ModelState.IsValid)
                return Page();

            AddArtist =  await artistCrud.AddArtist(input);

            return RedirectToPage("AllArtists");
        }
    }
}
