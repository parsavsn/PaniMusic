using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.Map.CrudDtos.AlbumTrack.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin.Albums
{
    public class NewAlbumTrackModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        private readonly ITrackCrud trackCrud;

        public NewAlbumTrackModel(IAlbumCrud albumCrud, ITrackCrud trackCrud)
        {
            this.albumCrud = albumCrud;

            this.trackCrud = trackCrud;
        }

        public Album Album { get; set; }

        [BindProperty]
        public AddAlbumTrackInput Input { get; set; }
        
        [TempData]
        public bool AddAlbumTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Album = await albumCrud.GetAlbumById(id);

            if (Album == null)
                return RedirectToPage("AllAlbums");

            TempData["AlbumId"] = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            AddAlbumTrack = await trackCrud.AddAlbumTrack(Input);

            return Redirect($"allalbumtracks?id={TempData["AlbumId"]}");
        }
    }
}
