using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.Artists
{
    [Authorize(Policy = "AdminPanel")]
    public class AllArtistsModel : PageModel
    {
        private readonly IArtistCrud artistCrud;

        public AllArtistsModel(IArtistCrud artistCrud)
        {
            this.artistCrud = artistCrud;
        }

        public List<Artist> AllArtists { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllArtists = await artistCrud.GetAllArtists();

            int skip = (page - 1) * 10;

            int countOfArtists = getAllArtists.Count;

            PageId = page;

            double countOfPages = (double)countOfArtists / 10;

            PageCount = Math.Ceiling(countOfPages);

            AllArtists = getAllArtists.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
