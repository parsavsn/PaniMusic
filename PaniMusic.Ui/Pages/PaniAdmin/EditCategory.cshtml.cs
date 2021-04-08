using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class EditCategoryModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public EditCategoryModel(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        [BindProperty]
        public UpdateGalleryCategoryInput Input { get; set; }

        public GalleryCategory GalleryCategory { get; set; }

        [TempData]
        public bool EditCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            GalleryCategory = await galleryCategoryCrud.GetGalleryCategoryById(id);

            if (GalleryCategory == null)
                return RedirectToPage("AllCategories");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            EditCategory = await galleryCategoryCrud.UpdateGalleryCategory(Input);

            return RedirectToPage("AllCategories");
        }
    }
}
