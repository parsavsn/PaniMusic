using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class FavoriteTracksModel : PageModel
    {
        private readonly IFavoriteTrackCrud favoriteTrackCrud;

        private readonly UserManager<User> userManager;

        public FavoriteTracksModel(IFavoriteTrackCrud favoriteTrackCrud, UserManager<User> userManager)
        {
            this.favoriteTrackCrud = favoriteTrackCrud;

            this.userManager = userManager;
        }

        public IEnumerable<FavoriteTrack> FavoriteTracks { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
        {
            var currentUser = await userManager.GetUserAsync(User);

            var favoriteTracks = await favoriteTrackCrud.GetFavoriteTracks(currentUser.Id);

            int skip = (page - 1) * 10;

            int countOfFavorites = favoriteTracks.Count();

            PageId = page;

            double countOfPages = (double)countOfFavorites / 10;

            PageCount = Math.Ceiling(countOfPages);

            FavoriteTracks = favoriteTracks.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
