using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.Album.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Albums
{
    [Authorize(Policy = "AdminPanel")]
    public class EditAlbumModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        public EditAlbumModel(IAlbumCrud albumCrud, IStyleCrud styleCrud, IArtistCrud artistCrud)
        {
            this.albumCrud = albumCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public UpdateAlbumInput Input { get; set; }

        public Album Album { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        [TempData]
        public bool EditAlbum { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Album = await albumCrud.GetAlbumById(id);

            if (Album == null)
                return RedirectToPage("AllAlbums");

            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name", Album.Style.Id);

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name", Album.Artist.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            EditAlbum = await albumCrud.UpdateAlbum(Input);

            return RedirectToPage("AllAlbums");
        }
    }
}
