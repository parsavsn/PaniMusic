using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Map.CrudDtos.Album.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewAlbumModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        public NewAlbumModel(IAlbumCrud albumCrud, IStyleCrud styleCrud, IArtistCrud artistCrud)
        {
            this.albumCrud = albumCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public AddAlbumInput Input { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        [TempData]
        public bool AddAlbum { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name");

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            AddAlbum = await albumCrud.AddAlbum(Input);

            return RedirectToPage("AllAlbums");
        }
    }
}
