using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;

namespace PaniMusic.Ui.Pages.PaniAdmin
{
    public class DeleteAlbumModel : PageModel
    {
        private readonly IAlbumCrud albumCrud;

        public DeleteAlbumModel(IAlbumCrud albumCrud)
        {
            this.albumCrud = albumCrud;
        }

        [TempData]
        public bool DeleteAlbum { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteAlbum = await albumCrud.DeleteAlbum(id);

            return RedirectToPage("AllAlbums");
        }
    }
}
