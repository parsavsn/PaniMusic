﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "UserPanel")]
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateFavoriteAlbum([FromBody] AddFavoriteAlbumInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createFavoriteAlbum = await favoriteAlbumCrud.AddFavoriteAlbum(input);

            if (createFavoriteAlbum == false)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteAlbum(int albumId, string userId)
        {
            var getFavoriteAlbum = await favoriteAlbumCrud.GetFavoriteAlbum(albumId, userId);

            if (getFavoriteAlbum == null)
                return NotFound();

            return Ok(getFavoriteAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteAlbums([FromQuery] string userId)
        {
            var getFavoriteAlbums = await favoriteAlbumCrud.GetFavoriteAlbums(userId);

            if (getFavoriteAlbums.Count() == 0)
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
