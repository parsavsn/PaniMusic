using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteImageModel : PageModel
    {
        private readonly IGalleryImageCrud galleryImageCrud;

        public DeleteImageModel(IGalleryImageCrud galleryImageCrud)
        {
            this.galleryImageCrud = galleryImageCrud;
        }

        [TempData]
        public bool DeleteImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string categoryId)
        {
            DeleteImage = await galleryImageCrud.DeleteGalleryImage(id);

            return Redirect($"categoryimages?id={categoryId}");
        }
    }
}
