using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.Track.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Tracks
{
    [Authorize(Policy = "AdminPanel")]
    public class EditTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        public EditTrackModel(ITrackCrud trackCrud, IStyleCrud styleCrud, IArtistCrud artistCrud)
        {
            this.trackCrud = trackCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public UpdateTrackInput Input { get; set; }

        public Track Track { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        [TempData]
        public bool EditTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Track = await trackCrud.GetTrackById(id);

            if (Track == null || Track.AlbumId != null)
                return RedirectToPage("AllTracks");

            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name", Track.Style.Id);

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name", Track.Artist.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            EditTrack = await trackCrud.UpdateTrack(Input);

            return RedirectToPage("AllTracks");
        }
    }
}
