using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.GalleryImage.Add;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud
{
    public class GalleryImageCrud : IGalleryImageCrud
    {
        private readonly IRepository<GalleryImage> galleryImageRepository;

        private readonly IRepository<GalleryCategory> galleryCategoryRepository;

        private readonly IMapper mapper;

        public GalleryImageCrud(IRepository<GalleryImage> galleryImageRepository
            , IRepository<GalleryCategory> galleryCategoryRepository
            , IMapper mapper)
        {
            this.galleryImageRepository = galleryImageRepository;

            this.galleryCategoryRepository = galleryCategoryRepository;

            this.mapper = mapper;
        }

        public async Task AddGalleryImage(AddGalleryImageInput addGalleryImageInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addGalleryImageInput.MyImage, addNewGuid);

            var newGalleryImage = mapper.Map<GalleryImage>(addGalleryImageInput);

            newGalleryImage.Image = addGalleryImageInput.MyImage.FileName;

            galleryImageRepository.Insert(newGalleryImage);

            await galleryImageRepository.Save();
        }

        public async Task<List<GalleryImage>> GetGalleryImages(string link)
        {
            var getGalleryCategory = await galleryCategoryRepository.GetQuery()
                .FirstOrDefaultAsync(galleryCategory => galleryCategory.Link == link);

            var getGalleryImages = await galleryImageRepository.GetQuery()
                .Where(galleryImage => galleryImage.GalleryCategoryId == getGalleryCategory.Id)
                .ToListAsync();

            return getGalleryImages;
        }

        public async Task DeleteGalleryImage(int id)
        {
            var getGalleryImage = await galleryCategoryRepository.Get(id);

            var pathImage = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploads/galleryimage",
                getGalleryImage.Image);

            File.Delete(pathImage);

            galleryCategoryRepository.Delete(id);

            await galleryCategoryRepository.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "galleryimage",
                myGuid + myFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await myFile.CopyToAsync(stream);
            }
        }
    }
}
