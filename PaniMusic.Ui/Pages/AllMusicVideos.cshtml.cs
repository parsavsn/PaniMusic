using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;

namespace PaniMusic.Ui.Pages
{
    public class AllMusicVideosModel : PageModel
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        public AllMusicVideosModel(IMusicVideoCrud musicVideoCrud)
        {
            this.musicVideoCrud = musicVideoCrud;
        }

        public List<MusicVideo> AllMusicVideo { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery]int page = 1)
        {
            // paging

            var getAllMusicVideos = await musicVideoCrud.GetAllMusicVideos();

            int skip = (page - 1) * 5;

            int countOfMusicVideos = getAllMusicVideos.Count;

            PageId = page;

            double countOfPages = (double)countOfMusicVideos / 5;

            PageCount = Math.Ceiling(countOfPages);

            AllMusicVideo = getAllMusicVideos.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(5)
                .ToList();

            return Page();
        }
    }
}
