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
        Task AddGalleryCategory(AddGalleryCategoryInput addGalleryCategoryInput);

        Task<GalleryCategory> GetGalleryCategory(string link);

        Task<List<GalleryCategory>> GetAllGalleryCategories();

        Task UpdateGalleryCategory(UpdateGalleryCategoryInput updateGalleryCategoryInput);

        Task DeleteGalleryCategory(int id);
    }
}
