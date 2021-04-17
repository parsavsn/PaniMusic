using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Search.SearchAlbums;

namespace PaniMusic.Ui.Pages
{
    public class SearchAlbumModel : PageModel
    {
        private readonly ISearchAlbums searchAlbums;

        public SearchAlbumModel(ISearchAlbums searchAlbums)
        {
            this.searchAlbums = searchAlbums;
        }

        public IEnumerable<Album> Albums { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] string name)
        {
            Albums = await searchAlbums.SearchAlbum(name);

            return Page();
        }
    }
}
