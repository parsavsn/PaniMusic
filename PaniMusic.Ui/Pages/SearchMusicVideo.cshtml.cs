using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Search.SearchMusicVideos;

namespace PaniMusic.Ui.Pages
{
    public class SearchMusicVideoModel : PageModel
    {
        private readonly ISearchMusicVideos searchMusicVideos;

        public SearchMusicVideoModel(ISearchMusicVideos searchMusicVideos)
        {
            this.searchMusicVideos = searchMusicVideos;
        }

        public IEnumerable<MusicVideo> MusicVideos { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] string name)
        {
            MusicVideos = await searchMusicVideos.SearchMusicVideo(name);

            return Page();
        }
    }
}
