using AutoMapper;
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

            await UploadFile(updateGalleryCategoryInput.MyImage, updateNewGuid);

            var getGalleryCategory = await galleryCategoryRepostiory.Get(updateGalleryCategoryInput.Id);

            var changeGalleryCategory = ChangeForUpdate(getGalleryCategory, updateGalleryCategoryInput);

            galleryCategoryRepostiory.Update(changeGalleryCategory);

            await galleryCategoryRepostiory.Save();
        }

        public async Task DeleteGalleryCategory(int id)
        {
            var getGalleryCategory = await galleryCategoryRepostiory.Get(id);

            DeleteFile(getGalleryCategory.Image);

            galleryCategoryRepostiory.Delete(id);

            await galleryCategoryRepostiory.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile.Length > 0)
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

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/gallerycategory",
                  fileName);

                File.Delete(filePath);
            }
        }
    }
}
