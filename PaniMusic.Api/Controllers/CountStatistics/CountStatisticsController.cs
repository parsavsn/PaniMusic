using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.CountStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.CountStatistics
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountStatisticsController : ControllerBase
    {
        private readonly ICountStatistics countStatistics;

        public CountStatisticsController(ICountStatistics countStatistics)
        {
            this.countStatistics = countStatistics;
        }

        [HttpGet]
        public async Task<IActionResult> CountOfTracks()
        {
            var countOfTracks = await countStatistics.CountOfTracks();

            return Ok(countOfTracks);
        }

        [HttpGet]
        public async Task<IActionResult> CountOfAlbums()
        {
            var countOfAlbums = await countStatistics.CountOfAlbums();

            return Ok(countOfAlbums);
        }

        [HttpGet]
        public async Task<IActionResult> CountOfMusicVideos()
        {
            var countOfMusicVideos = await countStatistics.CountOfMusicVideos();

            return Ok(countOfMusicVideos);
        }

        [HttpGet]
        public async Task<IActionResult> CountOfArtists()
        {
            var countOfArtists = await countStatistics.CountOfArtists();

            return Ok(countOfArtists);
        }

        [HttpGet]
        public async Task<IActionResult> CountOfUsers()
        {
            var countOfUsers = await countStatistics.CountOfUsers();

            return Ok(countOfUsers);
        }

        [HttpGet]
        public async Task<IActionResult> CountOfFeedbacks()
        {
            var countOfFeedbacks = await countStatistics.CountOfFeedbacks();

            return Ok(countOfFeedbacks);
        }
    }
}
