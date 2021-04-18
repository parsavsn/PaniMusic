using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud;
using PaniMusic.Services.Map.CrudDtos.FavoriteMusicVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteMusicVideoCrudController : ControllerBase
    {
        private readonly IFavoriteMusicVideoCrud favoriteMusicVideoCrud;

        public FavoriteMusicVideoCrudController(IFavoriteMusicVideoCrud favoriteMusicVideoCrud)
        {
            this.favoriteMusicVideoCrud = favoriteMusicVideoCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteMusicVideo([FromBody] AddFavoriteMusicVideoInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var favoriteMusicVideo = await favoriteMusicVideoCrud.AddFavoriteMusicVideo(input);

            if (favoriteMusicVideo == false)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteMusicVideo(int musicVideoId, string userId)
        {
            var getFavoriteMusicVideo = await favoriteMusicVideoCrud.GetFavoriteMusicVideo(musicVideoId, userId);

            if (getFavoriteMusicVideo == null)
                return NotFound();

            return Ok(getFavoriteMusicVideo);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteMusicVideos([FromQuery] string userId)
        {
            var getFavoriteMusicVideos = await favoriteMusicVideoCrud.GetFavoriteMusicVideos(userId);

            if (getFavoriteMusicVideos.Count() == 0)
                return NotFound();

            return Ok(getFavoriteMusicVideos);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteMusicVideo([FromQuery] int favoriteMusicVideoId)
        {
            var deleteFavoriteTrack = await favoriteMusicVideoCrud.DeleteFavoriteMusicVideo(favoriteMusicVideoId);

            if (deleteFavoriteTrack == false)
                return NotFound();

            return Ok();
        }
    }
}
