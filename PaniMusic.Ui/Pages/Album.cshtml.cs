using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;

namespace PaniMusic.Ui.Pages
{
    public class AlbumModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        public AlbumModel(IAlbumCrud albumCrud)
        {
            this.albumCrud = albumCrud;
        }

        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            Album = await albumCrud.GetAlbumByLink(link);

            if (Album == null)
                return RedirectToPage("AllAlbums");

            return Page();
        }
    }
}
