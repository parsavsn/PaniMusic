using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class FavoriteMusicVideosModel : PageModel
    {
        private readonly IFavoriteMusicVideoCrud favoriteMusicVideoCrud;

        private readonly UserManager<User> userManager;

        public FavoriteMusicVideosModel(IFavoriteMusicVideoCrud favoriteMusicVideoCrud, UserManager<User> userManager)
        {
            this.favoriteMusicVideoCrud = favoriteMusicVideoCrud;

            this.userManager = userManager;
        }

        public IEnumerable<FavoriteMusicVideo> FavoriteMusicVideos { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
        {
            var currentUser = await userManager.GetUserAsync(User);

            var favoriteMusicVideos = await favoriteMusicVideoCrud.GetFavoriteMusicVideos(currentUser.Id);

            int skip = (page - 1) * 10;

            int countOfFavorites = favoriteMusicVideos.Count();

            PageId = page;

            double countOfPages = (double)countOfFavorites / 10;

            PageCount = Math.Ceiling(countOfPages);

            FavoriteMusicVideos = favoriteMusicVideos.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
