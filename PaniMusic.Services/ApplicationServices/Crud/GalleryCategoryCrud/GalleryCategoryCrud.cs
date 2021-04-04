﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud
{
    public class GalleryCategoryCrud : IGalleryCategoryCrud
    {
        private readonly IRepository<GalleryCategory> galleryCategoryRepostiory;

        private readonly IMapper mapper;

        public GalleryCategoryCrud(IRepository<GalleryCategory> galleryCategoryRepostiory, IMapper mapper)
        {
            this.galleryCategoryRepostiory = galleryCategoryRepostiory;

            this.mapper = mapper;
        }

        public async Task AddGalleryCategory(AddGalleryCategoryInput addGalleryCategoryInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addGalleryCategoryInput.MyImage, addNewGuid);

            var newGalleryCategory = mapper.Map<GalleryCategory>(addGalleryCategoryInput);

            newGalleryCategory.Image = addNewGuid + addGalleryCategoryInput.MyImage.FileName;

            galleryCategoryRepostiory.Insert(newGalleryCategory);

            await galleryCategoryRepostiory.Save();
        }

        public async Task<GalleryCategory> GetGalleryCategory(string link)
        {
            var getGalleryCategory = await galleryCategoryRepostiory.GetQuery()
                .FirstOrDefaultAsync(galleryCategory => galleryCategory.Link == link);

            if (getGalleryCategory == null)
                return null;

            return getGalleryCategory;
        }

        public async Task<List<GalleryCategory>> GetAllGalleryCategories()
        {
            return await galleryCategoryRepostiory.GetAll();
        }

        public async Task UpdateGalleryCategory(UpdateGalleryCategoryInput updateGalleryCategoryInput)
        {
            var updateNewGuid = Guid.NewGuid().ToString();

            if (updateGalleryCategoryInput.MyImage.Length > 0)
                await UploadFile(updateGalleryCategoryInput.MyImage, updateNewGuid);

            var getGalleryCategory = await GetGalleryCategory(updateGalleryCategoryInput.Link);

            var changeGalleryCategory = ChangeForUpdate(getGalleryCategory, updateGalleryCategoryInput);

            galleryCategoryRepostiory.Update(changeGalleryCategory);

            await galleryCategoryRepostiory.Save();
        }

        public async Task DeleteGalleryCategory(string link)
        {
            var getGalleryCategory = await GetGalleryCategory(link);

            var pathImage = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploads/gallerycategory",
                getGalleryCategory.Image);

            File.Delete(pathImage);

            galleryCategoryRepostiory.Delete(getGalleryCategory.Id);

            await galleryCategoryRepostiory.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "gallerycategory",
                myGuid + myFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await myFile.CopyToAsync(stream);
            }
        }

        private GalleryCategory ChangeForUpdate(GalleryCategory category, UpdateGalleryCategoryInput input)
        {
            category.Name = input.Name;

            if (input.MyImage.Length > 0)
                category.Image = input.MyImage.FileName;

            category.Link = input.Link;

            category.TitleTag = input.TitleTag;

            category.MetaDescription = input.MetaDescription;

            category.MetaTag = input.MetaTag;

            return category;
        }
    }
}
