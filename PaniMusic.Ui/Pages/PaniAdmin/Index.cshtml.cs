using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.CountStatistics;
using PaniMusic.Services.ApplicationServices.VisitStatistics;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class IndexModel : PageModel
    {
        private readonly IVisitStatistics visitStatistics;

        private readonly ICountStatistics countStatistics;

        public IndexModel(IVisitStatistics visitStatistics, ICountStatistics countStatistics)
        {
            this.visitStatistics = visitStatistics;

            this.countStatistics = countStatistics;
        }

        public int CountOfTracks { get; set; }

        public int CountOfAlbums { get; set; }

        public int CountOfMusicVideos { get; set; }

        public int CountOfArtists { get; set; }

        public int CountOfUsers { get; set; }

        public int CountOfFeedbacks { get; set; }

        public int TrackVisitStatistics { get; set; }

        public int AlbumVisitStatistics { get; set; }

        public int MusicVideoVisitStatistics { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CountOfTracks = await countStatistics.CountOfTracks();

            CountOfAlbums = await countStatistics.CountOfAlbums();

            CountOfMusicVideos = await countStatistics.CountOfMusicVideos();

            CountOfArtists = await countStatistics.CountOfArtists();

            CountOfUsers = await countStatistics.CountOfUsers();

            CountOfFeedbacks = await countStatistics.CountOfFeedbacks();

            TrackVisitStatistics = await visitStatistics.TrackVisitStatistics();

            AlbumVisitStatistics = await visitStatistics.AlbumVisitStatistics();

            MusicVideoVisitStatistics = await visitStatistics.MusicVideoStatistics();

            return Page();
        }
    }
}
