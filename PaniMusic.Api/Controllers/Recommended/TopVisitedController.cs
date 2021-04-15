using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.AlbumTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.MusicVideoTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.TrackTopVisited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Recommended
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopVisitedController : ControllerBase
    {
        private readonly ITrackTopVisited trackTopVisited;

        private readonly IAlbumTopVisited albumTopVisited;

        private readonly IMusicVideoTopVisited musicVideoTopVisited;

        public TopVisitedController(ITrackTopVisited trackTopVisited
            , IAlbumTopVisited albumTopVisited
            , IMusicVideoTopVisited musicVideoTopVisited)
        {
            this.trackTopVisited = trackTopVisited;

            this.albumTopVisited = albumTopVisited;

            this.musicVideoTopVisited = musicVideoTopVisited;
        }

        [HttpGet]
        public async Task<IActionResult> TopVisitedTracks([FromQuery] int numberOfItems)
        {
            var getTopVisitedTracks = await trackTopVisited.TopVisitedTracks(numberOfItems);

            return Ok(getTopVisitedTracks);
        }

        [HttpGet]
        public async Task<IActionResult> TopVisitedAlbums([FromQuery] int numberOfItems)
        {
            var getTopVisitedAlbums = await albumTopVisited.TopVisitedAlbums(numberOfItems);

            return Ok(getTopVisitedAlbums);
        }

        [HttpGet]
        public async Task<IActionResult> TopVisitedMusicVideos([FromQuery] int numberOfItems)
        {
            var getTopVisitedMusicVideos = await musicVideoTopVisited.TopVisitedMusicVideos(numberOfItems);

            return Ok(getTopVisitedMusicVideos);
        }
    }
}
