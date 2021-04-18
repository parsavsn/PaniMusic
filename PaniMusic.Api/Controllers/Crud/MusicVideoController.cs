using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Add;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicVideoController : ControllerBase
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        public MusicVideoController(IMusicVideoCrud musicVideoCrud)
        {
            this.musicVideoCrud = musicVideoCrud;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateMusicVideo([FromForm] AddMusicVideoInput addMusicVideoInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await musicVideoCrud.AddMusicVideo(addMusicVideoInput);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetMusicVideoByLink(string link)
        {
            var getMusicVideo = await musicVideoCrud.GetMusicVideoByLink(link);

            if (getMusicVideo == null)
                return NotFound();

            return Ok(getMusicVideo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMusicVideoById([FromQuery] int id)
        {
            var getMusicVideo = await musicVideoCrud.GetMusicVideoById(id);

            if (getMusicVideo == null)
                return NotFound();

            return Ok(getMusicVideo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMusicVideos()
        {
            var getAllMusicVideos = await musicVideoCrud.GetAllMusicVideos();

            return Ok(getAllMusicVideos);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateMusicVideo([FromForm] UpdateMusicVideoInput updateMusicVideoInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateMusicVideo = await musicVideoCrud.UpdateMusicVideo(updateMusicVideoInput);

            if (updateMusicVideo == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteMusicVideo([FromQuery] int id)
        {
            var deleteMusicVideo = await musicVideoCrud.DeleteMusicVideo(id);

            if (deleteMusicVideo == false)
                return NotFound();

            return Ok();
        }
    }
}
