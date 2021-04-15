using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.Map.CrudDtos.AlbumTrack.Add;
using PaniMusic.Services.Map.CrudDtos.AlbumTrack.Update;
using PaniMusic.Services.Map.CrudDtos.Track.Add;
using PaniMusic.Services.Map.CrudDtos.Track.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackCrudController : ControllerBase
    {
        private readonly ITrackCrud trackCrud;

        public TrackCrudController(ITrackCrud trackCrud)
        {
            this.trackCrud = trackCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrack([FromForm] AddTrackInput addTrackInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await trackCrud.AddTrack(addTrackInput);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbumTrack([FromForm] AddAlbumTrackInput addAlbumTrackInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await trackCrud.AddAlbumTrack(addAlbumTrackInput);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetTrackByLink(string link)
        {
            var getTrack = await trackCrud.GetTrackByLink(link);

            if (getTrack == null)
                return NotFound();

            return Ok(getTrack);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrackById([FromQuery] int id)
        {
            var getTrack = await trackCrud.GetTrackById(id);

            if (getTrack == null)
                return NotFound();

            return Ok(getTrack);
        }

        [HttpGet]
        public async Task<IActionResult> GetTracksForAlbum([FromQuery] int albumId)
        {
            var getTracksForAlbum = await trackCrud.GetTracksForAlbum(albumId);

            if (getTracksForAlbum == null)
                return NotFound("Album not found");

            if (getTracksForAlbum.Count == 0)
                return NotFound("The relevant Album has no Tracks");

            return Ok(getTracksForAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTracks()
        {
            var getAllTracks = await trackCrud.GetAllTracks();

            return Ok(getAllTracks);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrack([FromForm] UpdateTrackInput updateTrackInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateTrack = await trackCrud.UpdateTrack(updateTrackInput);

            if (updateTrack == false)
                return NotFound();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbumTrack([FromForm] UpdateAlbumTrackInput updateAlbumTrackInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateAlbumTrack = await trackCrud.UpdateAlbumTrack(updateAlbumTrackInput);

            if (updateAlbumTrack == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrack([FromQuery] int id)
        {
            var deleteTrack = await trackCrud.DeleteTrack(id);

            if (deleteTrack == false)
                return NotFound();

            return Ok();
        }
    }
}
