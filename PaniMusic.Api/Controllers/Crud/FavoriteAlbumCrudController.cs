using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteAlbumCrud;
using PaniMusic.Services.Map.CrudDtos.FavoriteAlbum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteAlbumCrudController : ControllerBase
    {
        private readonly IFavoriteAlbumCrud favoriteAlbumCrud;

        public FavoriteAlbumCrudController(IFavoriteAlbumCrud favoriteAlbumCrud)
        {
            this.favoriteAlbumCrud = favoriteAlbumCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteAlbum([FromBody] AddFavoriteAlbumInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await favoriteAlbumCrud.AddFavoriteAlbum(input);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteAlbums([FromQuery] string userId)
        {
            var getFavoriteAlbums = await favoriteAlbumCrud.GetFavoriteAlbums(userId);

            if (getFavoriteAlbums.Count == 0)
                return NotFound();

            return Ok(getFavoriteAlbums);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteAlbum([FromQuery] int favoriteAlbumId)
        {
            var deleteFavoriteTrack = await favoriteAlbumCrud.DeleteFavoriteAlbum(favoriteAlbumId);

            if (deleteFavoriteTrack == false)
                return NotFound();

            return Ok();
        }
    }
}
