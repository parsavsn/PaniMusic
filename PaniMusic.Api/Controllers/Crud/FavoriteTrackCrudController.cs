using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud;
using PaniMusic.Services.Map.CrudDtos.FavoriteTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteTrackCrudController : ControllerBase
    {
        private readonly IFavoriteTrackCrud favoriteTrackCrud;

        public FavoriteTrackCrudController(IFavoriteTrackCrud favoriteTrackCrud)
        {
            this.favoriteTrackCrud = favoriteTrackCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteTrack([FromBody] AddFavoriteTrackInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await favoriteTrackCrud.AddFavoriteTrack(input);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteTracks([FromQuery] string userId)
        {
            var getFavoriteTracks = await favoriteTrackCrud.GetFavoriteTracks(userId);

            if (getFavoriteTracks.Count == 0)
                return NotFound();

            return Ok(getFavoriteTracks);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteTrack([FromQuery] int favoriteTrackId)
        {
            var deleteFavoriteTrack = await favoriteTrackCrud.DeleteFavoriteTrack(favoriteTrackId);

            if (deleteFavoriteTrack == false)
                return NotFound();

            return Ok();
        }
    }
}
