using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;

namespace PaniMusic.Ui.Pages
{
    public class TrackModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        public TrackModel(ITrackCrud trackCrud)
        {
            this.trackCrud = trackCrud;
        }

        public Track Track { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            Track = await trackCrud.GetTrackByLink(link);

            if (Track == null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
