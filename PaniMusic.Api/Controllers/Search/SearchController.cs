using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Search.SearchAlbums;
using PaniMusic.Services.ApplicationServices.Search.SearchMusicVideos;
using PaniMusic.Services.ApplicationServices.Search.SearchTracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Search
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchTracks searchTracks;

        private readonly ISearchAlbums searchAlbums;

        private readonly ISearchMusicVideos searchMusicVideos;

        public SearchController(ISearchTracks searchTracks
            , ISearchAlbums searchAlbums
            , ISearchMusicVideos searchMusicVideos)
        {
            this.searchTracks = searchTracks;

            this.searchAlbums = searchAlbums;

            this.searchMusicVideos = searchMusicVideos;
        }

        [HttpGet]
        public async Task<IActionResult> SearchTrack([FromQuery] string trackName)
        {
            var getTrack = await searchTracks.SearchTrack(trackName);

            if (getTrack == null)
                return NotFound();

            return Ok(getTrack);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAlbum([FromQuery] string albumName)
        {
            var getAlbum = await searchAlbums.SearchAlbum(albumName);

            if (getAlbum == null)
                return NotFound();

            return Ok(getAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> SearchMusicVideo([FromQuery] string musicVideoName)
        {
            var getMusicVideo = await searchMusicVideos.SearchMusicVideo(musicVideoName);

            if (getMusicVideo == null)
                return NotFound();

            return Ok(getMusicVideo);
        }
    }
}
