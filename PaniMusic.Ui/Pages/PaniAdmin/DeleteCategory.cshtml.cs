using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public DeleteCategoryModel(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        [TempData]
        public bool DeleteCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteCategory = await galleryCategoryCrud.DeleteGalleryCategory(id);

            return RedirectToPage("AllCategories");
        }
    }
}
