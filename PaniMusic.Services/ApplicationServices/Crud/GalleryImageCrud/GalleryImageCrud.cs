﻿using AutoMapper;
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

        public async Task<bool> AddGalleryImage(AddGalleryImageInput addGalleryImageInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addGalleryImageInput.MyImage, addNewGuid);

            var newGalleryImage = mapper.Map<GalleryImage>(addGalleryImageInput);

            newGalleryImage.Image = addNewGuid + "-" + addGalleryImageInput.MyImage.FileName;

            galleryImageRepository.Insert(newGalleryImage);

            await galleryImageRepository.Save();

            return true;
        }

        public async Task<List<GalleryImage>> GetGalleryImages(int categoryId)
        {
            var getGalleryCategory = await galleryCategoryRepository.Get(categoryId);

            var getGalleryImages = await galleryImageRepository.GetQuery()
                .Include(galleryImage => galleryImage.GalleryCategory)
                .Where(galleryImage => galleryImage.GalleryCategoryId == getGalleryCategory.Id)
                .ToListAsync();

            return getGalleryImages;
        }

        public async Task<bool> DeleteGalleryImage(int id)
        {
            var getGalleryImage = await galleryImageRepository.Get(id);

            DeleteFile(getGalleryImage.Image);

            galleryImageRepository.Delete(id);

            await galleryImageRepository.Save();

            return true;
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "galleryimage",
                myGuid + "-" + myFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
            }
        }

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/galleryimage",
                  fileName);

                File.Delete(filePath);
            }
        }
    }
}
