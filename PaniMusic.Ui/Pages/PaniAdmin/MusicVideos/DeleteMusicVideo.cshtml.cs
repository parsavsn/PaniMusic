using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin.MusicVideos
{
    [Authorize(Policy = "AdminPanel")]
    [Authorize(Policy = "DeleteItem")]
    public class DeleteMusicVideoModel : PageModel
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        public DeleteMusicVideoModel(IMusicVideoCrud musicVideoCrud)
        {
            this.musicVideoCrud = musicVideoCrud;
        }

        [TempData]
        public bool DeleteMusicVideo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteMusicVideo = await musicVideoCrud.DeleteMusicVideo(id);

            return RedirectToPage("AllMusicVideos");
        }
    }
}
