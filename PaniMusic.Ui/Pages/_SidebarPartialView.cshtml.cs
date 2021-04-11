using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.AlbumTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.MusicVideoTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.TrackTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.AlbumTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.MusicVideoTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.TrackTopVisited;
using PaniMusic.Services.Map.RecommendedDtos;

namespace PaniMusic.Ui.Pages
{
    public class _SidebarPartialViewModel : PageModel
    {
        private readonly ITrackTopVisited trackTopVisited;

        private readonly IAlbumTopVisited albumTopVisited;

        private readonly IMusicVideoTopVisited musicVideoTopVisited;

        private readonly ITrackTopRated trackTopRated;

        private readonly IAlbumTopRated albumTopRated;

        private readonly IMusicVideoTopRated musicVideoTopRated;

        private readonly IStyleCrud styleCrud;

        public _SidebarPartialViewModel(ITrackTopVisited trackTopVisited
            , IAlbumTopVisited albumTopVisited
            , IMusicVideoTopVisited musicVideoTopVisited
            , ITrackTopRated trackTopRated
            , IAlbumTopRated albumTopRated
            , IMusicVideoTopRated musicVideoTopRated
            , IStyleCrud styleCrud)
        {
            this.trackTopVisited = trackTopVisited;

            this.albumTopVisited = albumTopVisited;

            this.musicVideoTopVisited = musicVideoTopVisited;

            this.trackTopRated = trackTopRated;

            this.albumTopRated = albumTopRated;

            this.musicVideoTopRated = musicVideoTopRated;

            this.styleCrud = styleCrud;
        }

        public List<RecommendedOutput> TrackTopVisited { get; set; }

        public List<RecommendedOutput> AlbumTopVisited { get; set; }

        public List<RecommendedOutput> MusicVideoTopVisited { get; set; }

        public List<RecommendedOutput> TrackTopRated { get; set; }

        public List<RecommendedOutput> AlbumTopRated { get; set; }

        public List<RecommendedOutput> MusicVideoTopRated { get; set; }

        public List<Style> Styles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TrackTopVisited = await trackTopVisited.TopVisitedTracks(6);

            AlbumTopVisited = await albumTopVisited.TopVisitedAlbums(6);

            MusicVideoTopVisited = await musicVideoTopVisited.TopVisitedMusicVideos(6);

            TrackTopRated = await trackTopRated.TopRatedTracks(6);

            AlbumTopRated = await albumTopRated.TopRatedAlbums(6);

            MusicVideoTopRated = await musicVideoTopRated.TopRatedMusicVideos(6);

            Styles = await styleCrud.GetAllStyles();

            return Page();
        }
    }
}
