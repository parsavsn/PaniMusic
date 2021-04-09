using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Images
{
    public class AllCategoriesModel : PageModel
    {
        private readonly IGalleryCategoryCrud galleryCategoryCrud;

        public AllCategoriesModel(IGalleryCategoryCrud galleryCategoryCrud)
        {
            this.galleryCategoryCrud = galleryCategoryCrud;
        }

        public List<GalleryCategory> AllGalleryCategories { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllGalleryCategories = await galleryCategoryCrud.GetAllGalleryCategories();

            int skip = (page - 1) * 10;

            int countOfGalleryCategories = getAllGalleryCategories.Count;

            PageId = page;

            double countOfPages = (double)countOfGalleryCategories / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllGalleryCategories = getAllGalleryCategories.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
