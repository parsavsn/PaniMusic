using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITrackCrud trackCrud;

        public IndexModel(ITrackCrud trackCrud)
        {
            this.trackCrud = trackCrud;
        }

        public List<Track> AllTracks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            var getAllTracks = await trackCrud.GetAllTracks();

            int skip = (page - 1) * 5;

            int countOfTracks = getAllTracks.Count;

            PageId = page;

            double countOfPages = (double)countOfTracks / 5;

            PageCount = Math.Ceiling(countOfPages);

            AllTracks = getAllTracks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(5)
                .ToList();

            return Page();
        }
    }
}
