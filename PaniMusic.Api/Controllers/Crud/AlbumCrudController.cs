using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.Map.CrudDtos.Album.Add;
using PaniMusic.Services.Map.CrudDtos.Album.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumCrudController : ControllerBase
    {
        private readonly IAlbumCrud albumCrud;

        public AlbumCrudController(IAlbumCrud albumCrud)
        {
            this.albumCrud = albumCrud;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateAlbum([FromForm] AddAlbumInput addAlbumInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await albumCrud.AddAlbum(addAlbumInput);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetAlbumByLink(string link)
        {
            var getAlbum = await albumCrud.GetAlbumByLink(link);

            if (getAlbum == null)
                return NotFound();

            return Ok(getAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbumById([FromQuery] int id)
        {
            var getAlbum = await albumCrud.GetAlbumById(id);

            if (getAlbum == null)
                return NotFound();

            return Ok(getAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            var getAllAlbums = await albumCrud.GetAllAlbums();

            return Ok(getAllAlbums);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateAlbum([FromForm] UpdateAlbumInput updateAlbumInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateAlbum = await albumCrud.UpdateAlbum(updateAlbumInput);

            if (updateAlbum == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteAlbum([FromQuery] int id)
        {
            var deleteAlbum = await albumCrud.DeleteAlbum(id);

            if (deleteAlbum == false)
                return NotFound();

            return Ok();
        }
    }
}
