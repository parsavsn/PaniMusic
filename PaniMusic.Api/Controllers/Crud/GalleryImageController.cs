using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;
using PaniMusic.Services.Map.CrudDtos.GalleryImage.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GalleryImageController : ControllerBase
    {
        private readonly IGalleryImageCrud galleryImageCrud;

        public GalleryImageController(IGalleryImageCrud galleryImageCrud)
        {
            this.galleryImageCrud = galleryImageCrud;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGalleryImage([FromForm] AddGalleryImageInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await galleryImageCrud.AddGalleryImage(input);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryImages([FromQuery] int categoryId)
        {
            var getGalleryImages = await galleryImageCrud.GetGalleryImages(categoryId);

            if (getGalleryImages == null)
                return NotFound("دسته بندی مورد نظر پیدا نشد");

            if (getGalleryImages.Count == 0)
                return NotFound("تصویری برای دسته بندی مورد نظر پیدا نشد");

            return Ok(getGalleryImages);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGalleryImage([FromQuery] int id)
        {
            var deleteGalleryImage = await galleryImageCrud.DeleteGalleryImage(id);

            if (deleteGalleryImage == false)
                return NotFound();

            return Ok();
        }
    }
}
