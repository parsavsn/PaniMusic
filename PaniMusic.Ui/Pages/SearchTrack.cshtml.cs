using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Search.SearchTracks;

namespace PaniMusic.Ui.Pages
{
    public class SearchTrackModel : PageModel
    {
        private readonly ISearchTracks searchTracks;

        public SearchTrackModel(ISearchTracks searchTracks)
        {
            this.searchTracks = searchTracks;
        }

        public IEnumerable<Track> Tracks { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] string name)
        {
            Tracks = await searchTracks.SearchTrack(name);

            return Page();
        }
    }
}
