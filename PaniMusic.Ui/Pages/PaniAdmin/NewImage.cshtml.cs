using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;
using PaniMusic.Services.Map.CrudDtos.GalleryImage.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewImageModel : PageModel
    {
        private readonly IGalleryImageCrud galleryImageCrud;

        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public NewImageModel(IGalleryImageCrud galleryImageCrud, IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryImageCrud = galleryImageCrud;

            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        [BindProperty]
        public AddGalleryImageInput Input { get; set; }

        public GalleryCategory GalleryCategory { get; set; }

        [TempData]
        public bool AddImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TempData["CategoryId"] = id;

            GalleryCategory = await galleryCategoryCrud.GetGalleryCategoryById(id);

            if (GalleryCategory == null)
                return RedirectToPage("AllCategories");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(AddGalleryImageInput input)
        {
            if (!ModelState.IsValid)
                return Page();

            AddImage = await galleryImageCrud.AddGalleryImage(input);

            return Redirect($"categoryimages?id={TempData["CategoryId"]}");
        }
    }
}
