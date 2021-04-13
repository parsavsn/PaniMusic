using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Images
{
    [Authorize(Policy = "AdminPanel")]
    public class CategoryImagesModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        private readonly IGalleryImageCrud galleryImageCrud;

        public CategoryImagesModel(IGalleryCategoryCrud galleryCategoryCrud, IGalleryImageCrud galleryImageCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;

            this.galleryImageCrud = galleryImageCrud;
        }

        public GalleryCategory GalleryCategory { get; set; }

        public List<GalleryImage> GalleryImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            GalleryCategory = await galleryCategoryCrud.GetGalleryCategoryById(id);

            if (GalleryCategory == null)
                return RedirectToPage("AllCategories");

            GalleryImages = await galleryImageCrud.GetGalleryImages(id);

            return Page();
        }
    }
}
