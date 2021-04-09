using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Add;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class NewMusicVideoModel : PageModel
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        public NewMusicVideoModel(IMusicVideoCrud musicVideoCrud, IStyleCrud styleCrud, IArtistCrud artistCrud)
        {
            this.musicVideoCrud = musicVideoCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public AddMusicVideoInput Input { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        [TempData]
        public bool AddMusicVideo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name");

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            AddMusicVideo = await musicVideoCrud.AddMusicVideo(Input);

            return RedirectToPage("AllMusicVideos");
        }
    }
}
