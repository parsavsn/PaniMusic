using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Update;

namespace PaniMusic.Ui.Pages.PaniAdmin.MusicVideos
{
    public class EditMusicVideoModel : PageModel
    {
        private readonly IMusicVideoCrud musicVideoCrud;

        private readonly IStyleCrud styleCrud;

        private readonly IArtistCrud artistCrud;

        public EditMusicVideoModel(IMusicVideoCrud musicVideoCrud, IStyleCrud styleCrud, IArtistCrud artistCrud)
        {
            this.musicVideoCrud = musicVideoCrud;

            this.styleCrud = styleCrud;

            this.artistCrud = artistCrud;
        }

        [BindProperty]
        public UpdateMusicVideoInput Input { get; set; }

        public MusicVideo MusicVideo { get; set; }

        public SelectList ListOfStyles { get; set; }

        public SelectList ListOfArtists { get; set; }

        [TempData]
        public bool EditMusicVideo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MusicVideo = await musicVideoCrud.GetMusicVideoById(id);

            if (MusicVideo == null)
                return RedirectToPage("AllMusicVideos");

            var allStyles = await styleCrud.GetAllStyles();

            ListOfStyles = new SelectList(allStyles, "Id", "Name", MusicVideo.Style.Id);

            var allArtists = await artistCrud.GetAllArtists();

            ListOfArtists = new SelectList(allArtists, "Id", "Name", MusicVideo.Artist.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            EditMusicVideo = await musicVideoCrud.UpdateMusicVideo(Input);

            return RedirectToPage("AllMusicVideos");
        }
    }
}
