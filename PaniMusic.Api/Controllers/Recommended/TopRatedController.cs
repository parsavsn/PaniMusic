using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.AlbumTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.MusicVideoTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.TrackTopRated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Recommended
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopRatedController : ControllerBase
    {
        private readonly ITrackTopRated trackTopRated;

        private readonly IAlbumTopRated albumTopRated;

        private readonly IMusicVideoTopRated musicVideoTopRated;

        public TopRatedController(ITrackTopRated trackTopRated
            , IAlbumTopRated albumTopRated
            , IMusicVideoTopRated musicVideoTopRated)
        {
            this.trackTopRated = trackTopRated;

            this.albumTopRated = albumTopRated;

            this.musicVideoTopRated = musicVideoTopRated;
        }

        [HttpGet]
        public async Task<IActionResult> TopRatedTracks([FromQuery] int numberOfItems)
        {
            var getTopRatedTracks = await trackTopRated.TopRatedTracks(numberOfItems);

            return Ok(getTopRatedTracks);
        }

        [HttpGet]
        public async Task<IActionResult> TopRatedAlbums([FromQuery] int numberOfItems)
        {
            var getTopRatedAlbums = await albumTopRated.TopRatedAlbums(numberOfItems);

            return Ok(getTopRatedAlbums);
        }

        [HttpGet]
        public async Task<IActionResult> TopRatedMusicVideos([FromQuery] int nuberOfItems)
        {
            var topRatedMusicVideos = await musicVideoTopRated.TopRatedMusicVideos(nuberOfItems);

            return Ok(topRatedMusicVideos);
        }
    }
}
