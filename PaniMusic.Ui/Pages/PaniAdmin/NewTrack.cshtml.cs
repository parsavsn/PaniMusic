using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.Map.CrudDtos.Track.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        private readonly IAlbumCrud albumCrud;

        public NewTrackModel(ITrackCrud trackCrud
            , IStyleCrud styleCrud
            , IArtistCrud artistCrud
            , IAlbumCrud albumCrud)
        {
            this.trackCrud = trackCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;

            this.albumCrud = albumCrud;
        }

        [BindProperty]
        public AddTrackInput Input { get; set; }

        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        public SelectList ListOfAlbums { get; set; }

        [TempData]
        public bool AddTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int? albumId)
        {
            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name");

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name");

            AlbumId = albumId;

            if (albumId != null)
            {
                var allAlbums = await albumCrud.GetAllAlbums();

                var getAlbum = await albumCrud.GetAlbumById((int)albumId);

                ListOfAlbums = new SelectList(allAlbums, "Id", "Name", getAlbum.Id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            AddTrack = await trackCrud.AddTrack(Input);

            return RedirectToPage("AllTracks");
        }
    }
}
