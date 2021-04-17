using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteAlbumCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class DeleteFavoriteAlbumModel : PageModel
    {
        private readonly IFavoriteAlbumCrud favoriteAlbumCrud;

        public DeleteFavoriteAlbumModel(IFavoriteAlbumCrud favoriteAlbumCrud)
        {
            this.favoriteAlbumCrud = favoriteAlbumCrud;
        }

        [TempData]
        public bool DeleteFavoriteAlbum { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteFavoriteAlbum = await favoriteAlbumCrud.DeleteFavoriteAlbum(id);

            return RedirectToPage("FavoriteAlbums");
        }
    }
}
