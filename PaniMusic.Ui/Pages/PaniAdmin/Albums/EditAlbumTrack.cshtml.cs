using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.AlbumTrack.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.Albums
{
    [Authorize(Policy = "AdminPanel")]
    public class EditAlbumTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        private readonly IAlbumCrud albumCrud;

        public EditAlbumTrackModel(ITrackCrud trackCrud, IAlbumCrud albumCrud)
        {
            this.trackCrud = trackCrud;

            this.albumCrud = albumCrud;
        }

        [BindProperty]
        public UpdateAlbumTrackInput Input { get; set; }

        public Track Track { get; set; }

        public Album Album { get; set; }

        [TempData]
        public bool EditAlbumTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int trackId,int albumId)
        {
            Track = await trackCrud.GetTrackById(trackId);

            Album = await albumCrud.GetAlbumById(albumId);

            if (Track == null || Album == null)
                return RedirectToPage("AllAlbums");

            TempData["AlbumId"] = albumId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.EditItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            EditAlbumTrack = await trackCrud.UpdateAlbumTrack(Input);

            return Redirect($"allalbumtracks?id={TempData["AlbumId"]}");
        }
    }
}
