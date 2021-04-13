using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;

namespace PaniMusic.Ui.Pages
{
    public class StyleModel : PageModel
    {
        private readonly IStyleCrud styleCrud;

        public StyleModel(IStyleCrud styleCrud)
        {
            this.styleCrud = styleCrud;
        }

        public Style Style { get; set; }

        public List<Track> StyleTracks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync(string link, [FromQuery]int page = 1)
        {
            Style = await styleCrud.GetStyleByLink(link);

            if (Style == null)
                return RedirectToPage("Index");

            // paging

            var getAllStyleTracks = Style.Tracks.Where(x => x.AlbumId == null).ToList();

            int skip = (page - 1) * 5;

            int countOfStyleTracks = getAllStyleTracks.Count;

            PageId = page;

            double countOfPages = (double)countOfStyleTracks / 5;

            PageCount = Math.Ceiling(countOfPages);

            StyleTracks = getAllStyleTracks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(5)
                .ToList();

            return Page();
        }
    }
}
