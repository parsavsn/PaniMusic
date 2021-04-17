using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class DeleteFavoriteMusicVideoModel : PageModel
    {
        private readonly IFavoriteMusicVideoCrud favoriteMusicVideoCrud;

        public DeleteFavoriteMusicVideoModel(IFavoriteMusicVideoCrud favoriteMusicVideoCrud)
        {
            this.favoriteMusicVideoCrud = favoriteMusicVideoCrud;
        }

        [TempData]
        public bool DeleteFavoriteMusicVideo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeleteFavoriteMusicVideo = await favoriteMusicVideoCrud.DeleteFavoriteMusicVideo(id);

            return RedirectToPage("FavoriteMusicVideos");
        }
    }
}
