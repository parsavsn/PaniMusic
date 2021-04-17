using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class DeleteFavoriteTrackModel : PageModel
    {
        private readonly IFavoriteTrackCrud favoriteTrackCrud;

        public DeleteFavoriteTrackModel(IFavoriteTrackCrud favoriteTrackCrud)
        {
            this.favoriteTrackCrud = favoriteTrackCrud;
        }

        [TempData]
        public bool DeleteFavoriteTrack { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteFavoriteTrack = await favoriteTrackCrud.DeleteFavoriteTrack(id);

            return RedirectToPage("FavoriteTracks");
        }
    }
}
