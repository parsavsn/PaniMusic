using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;
using PaniMusic.Services.Map.CrudDtos.Artist.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistCrudController : ControllerBase
    {
        private readonly IArtistCrud artistCrud;

        public ArtistCrudController(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }
        
        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateArtist([FromForm] AddArtistInput addArtistInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await artistCrud.AddArtist(addArtistInput);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetArtistByLink(string link)
        {
            var getArtist = await artistCrud.GetArtistByLink(link);

            if (getArtist == null)
                return NotFound();

            return Ok(getArtist);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtistById([FromQuery] int id)
        {
            var getArtist = await artistCrud.GetArtistById(id);

            if (getArtist == null)
                return NotFound();

            return Ok(getArtist);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var getAllArtists = await artistCrud.GetAllArtists();

            return Ok(getAllArtists);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateArtist([FromForm] UpdateArtistInput updateArtistInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateArtist = await artistCrud.UpdateArtist(updateArtistInput);

            if (updateArtist == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteArtist([FromQuery] int id)
        {
            var deleteArtist = await artistCrud.DeleteArtist(id);

            if (deleteArtist == false)
                return NotFound();

            return Ok();
        }
    }
}
