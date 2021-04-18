using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.VisitStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.VisitStatistics
{
    [Authorize(Policy = "AdminPanel")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitStatisticsController : ControllerBase
    {
        private readonly IVisitStatistics visitStatistics;

        public VisitStatisticsController(IVisitStatistics visitStatistics)
        {
            this.visitStatistics = visitStatistics;
        }

        [HttpGet]
        public async Task<IActionResult> TrackVisitStatistics()
        {
            var getTrackVisits = await visitStatistics.TrackVisitStatistics();

            return Ok(getTrackVisits);
        }

        [HttpGet]
        public async Task<IActionResult> AlbumVisitStatistics()
        {
            var getAlbumVisits = await visitStatistics.AlbumVisitStatistics();

            return Ok(getAlbumVisits);
        }

        [HttpGet]
        public async Task<IActionResult> MusicVideoVisitStatistics()
        {
            var getMusicVideoVisits = await visitStatistics.MusicVideoStatistics();

            return Ok(getMusicVideoVisits);
        }
    }
}
