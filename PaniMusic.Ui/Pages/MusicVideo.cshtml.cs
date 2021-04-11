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
    public class MusicVideoModel : PageModel
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        public MusicVideoModel(IMusicVideoCrud musicVideoCrud)
        {
            this.musicVideoCrud = musicVideoCrud;
        }

        public MusicVideo MusicVideo { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            MusicVideo = await musicVideoCrud.GetMusicVideoByLink(link);

            if (MusicVideo == null)
                return RedirectToPage("AllMusicVideos");

            return Page();
        }
    }
}
