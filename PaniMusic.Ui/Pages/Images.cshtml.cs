using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;

namespace PaniMusic.Ui.Pages
{
    public class ImagesModel : PageModel
    {
        private readonly IGalleryImageCrud galleryImageCrud;

        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public ImagesModel(IGalleryImageCrud galleryImageCrud, IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryImageCrud = galleryImageCrud;

            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        public List<GalleryImage> GalleryImages { get; set; }

        public GalleryCategory GalleryCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            GalleryCategory = await galleryCategoryCrud.GetGalleryCategoryByLink(link);

            if (GalleryCategory == null)
                return RedirectToPage("Gallery");

            GalleryImages = await galleryImageCrud.GetGalleryImages(GalleryCategory.Id);

            return Page();
        }
    }
}
