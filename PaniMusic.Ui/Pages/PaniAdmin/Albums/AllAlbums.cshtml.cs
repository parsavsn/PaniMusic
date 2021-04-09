using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Albums
{
    public class AllAlbumsModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        public AllAlbumsModel(IAlbumCrud albumCrud)
        {
            this.albumCrud = albumCrud;
        }

        public List<Album> AllAlbums { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            // paging

            var getAllAlbums = await albumCrud.GetAllAlbums();

            int skip = (page - 1) * 10;

            int countOfAlbums = getAllAlbums.Count;

            PageId = page;

            double countOfPages = (double)countOfAlbums / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllAlbums = getAllAlbums.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
