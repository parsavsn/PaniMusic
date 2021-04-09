using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Tracks
{
    public class AllTracksModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        public AllTracksModel(ITrackCrud trackCrud)
        {
            this.trackCrud = trackCrud;
        }

        public List<Track> AllTracks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllTracks = await trackCrud.GetAllTracks();

            int skip = (page - 1) * 10;

            int countOfTracks = getAllTracks.Count;

            PageId = page;

            double countOfPages = (double)countOfTracks / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllTracks = getAllTracks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
