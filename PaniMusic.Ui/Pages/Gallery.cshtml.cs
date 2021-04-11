using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;

namespace PaniMusic.Ui.Pages
{
    public class GalleryModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public GalleryModel(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        public List<GalleryCategory> GalleryCategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GalleryCategories = await galleryCategoryCrud.GetAllGalleryCategories();

            return Page();
        }
    }
}
