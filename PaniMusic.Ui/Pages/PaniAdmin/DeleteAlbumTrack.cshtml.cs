using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteAlbumTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        private readonly IAlbumCrud albumCrud;

        public DeleteAlbumTrackModel(ITrackCrud trackCrud, IAlbumCrud albumCrud)
        {
            this.trackCrud = trackCrud;

            this.albumCrud = albumCrud;
        }

        public Track Track { get; set; }

        public Album Album { get; set; }

        [TempData]
        public bool DeleteAlbumTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int trackId, int albumId)
        {
            Track = await trackCrud.GetTrackById(trackId);

            Album = await albumCrud.GetAlbumById(albumId);

            if (Track == null || Album == null)
                return RedirectToPage("AllAlbums");

            DeleteAlbumTrack = await trackCrud.DeleteTrack(trackId);

            return Redirect($"allalbumtracks?id={albumId}");
        }
    }
}
