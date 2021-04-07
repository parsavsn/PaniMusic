using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud
{
    public interface IGalleryCategoryCrud
    {
        Task<bool> AddGalleryCategory(AddGalleryCategoryInput addGalleryCategoryInput);

        Task<GalleryCategory> GetGalleryCategoryByLink(string link);

        Task<GalleryCategory> GetGalleryCategoryById(int id);

        Task<List<GalleryCategory>> GetAllGalleryCategories();

        Task<bool> UpdateGalleryCategory(UpdateGalleryCategoryInput updateGalleryCategoryInput);

        Task<bool> DeleteGalleryCategory(int id);
    }
}
