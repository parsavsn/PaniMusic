using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.Identity;
using PaniMusic.Services.Map.CrudDtos.GalleryCategory.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin.Images
{
    [Authorize(Policy = "AdminPanel")]
    public class NewCategoryModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public NewCategoryModel(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        [BindProperty]
        public AddGalleryCategoryInput Input { get; set; }

        [TempData]
        public bool AddCategory { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim(PaniClaims.NewItem, true.ToString()))
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
                return Page();

            AddCategory = await galleryCategoryCrud.AddGalleryCategory(Input);

            return RedirectToPage("AllCategories");
        }
    }
}
