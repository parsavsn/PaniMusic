using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Tracks
{
    public class DeleteTrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        public DeleteTrackModel(ITrackCrud trackCrud)
        {
            this.trackCrud = trackCrud;
        }

        [TempData]
        public bool DeleteTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteTrack = await trackCrud.DeleteTrack(id);

            return RedirectToPage("AllTracks");
        }
    }
}
