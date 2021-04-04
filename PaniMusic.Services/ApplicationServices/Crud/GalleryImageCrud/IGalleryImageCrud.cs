using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.GalleryImage.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud
{
    public interface IGalleryImageCrud
    {
        //we don't need getall and update service for this model.

        Task AddGalleryImage(AddGalleryImageInput addGalleryImageInput);

        Task<List<GalleryImage>> GetGalleryImages(string link);

        Task DeleteGalleryImage(int id);
    }
}
