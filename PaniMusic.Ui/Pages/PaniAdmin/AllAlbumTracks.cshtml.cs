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
    public class AllAlbumTracksModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        private readonly ITrackCrud trackCrud;

        public AllAlbumTracksModel(IAlbumCrud albumCrud, ITrackCrud trackCrud)
        {
            this.albumCrud = albumCrud;

            this.trackCrud = trackCrud;
        }

        public Album Album { get; set; }

        public List<Track> Tracks { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Album = await albumCrud.GetAlbumById(id);

            if (Album == null)
                return RedirectToPage("AllAlbums");

            Tracks = await trackCrud.GetTracksForAlbum(id);

            return Page();
        }
    }
}
