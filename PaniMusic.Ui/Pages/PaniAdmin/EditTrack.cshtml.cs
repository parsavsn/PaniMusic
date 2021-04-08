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
using PaniMusic.Services.Map.CrudDtos.Track.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class EditTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        private readonly IAlbumCrud albumCrud;

        public EditTrackModel(ITrackCrud trackCrud
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
        public UpdateTrackInput Input { get; set; }

        public Track Track { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        public SelectList ListOfAlbums { get; set; }

        [TempData]
        public bool EditTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Track = await trackCrud.GetTrackById(id);

            if (Track == null)
                return RedirectToPage("AllTracks");

            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name", Track.Style.Id);

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name", Track.Artist.Id);

            if (Track.AlbumId != null)
            {
                var allAlbums = await albumCrud.GetAllAlbums();

                ListOfAlbums = new SelectList(allAlbums, "Id", "Name", Track.AlbumId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            EditTrack = await trackCrud.UpdateTrack(Input);

            return RedirectToPage("AllTracks");
        }
    }
}
