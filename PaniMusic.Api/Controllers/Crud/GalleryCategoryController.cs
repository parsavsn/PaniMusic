using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GalleryCategoryController : ControllerBase
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public GalleryCategoryController(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "NewItem")]
        public async Task<IActionResult> CreateGalleryCategory([FromForm] AddGalleryCategoryInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await galleryCategoryCrud.AddGalleryCategory(input);

            return Ok();
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetGalleryCategoryByLink(string link)
        {
            var getGalleryCategory = await galleryCategoryCrud.GetGalleryCategoryByLink(link);

            if (getGalleryCategory == null)
                return NotFound();

            return Ok(getGalleryCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryCategoryById([FromQuery] int id)
        {
            var getGalleryCategory = await galleryCategoryCrud.GetGalleryCategoryById(id);

            if (getGalleryCategory == null)
                return NotFound();

            return Ok(getGalleryCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGalleryCategories()
        {
            var getAllGalleryCategories = await galleryCategoryCrud.GetAllGalleryCategories();

            return Ok(getAllGalleryCategories);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "EditItem")]
        public async Task<IActionResult> UpdateGalleryCategory([FromForm] UpdateGalleryCategoryInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateGalleryCategory = await galleryCategoryCrud.UpdateGalleryCategory(input);

            if (updateGalleryCategory == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "DeleteItem")]
        public async Task<IActionResult> DeleteGalleryCategory([FromQuery] int id)
        {
            var deleteGalleryCategory = await galleryCategoryCrud.DeleteGalleryCategory(id);

            if (deleteGalleryCategory == false)
                return NotFound();

            return Ok();
        }
    }
}
