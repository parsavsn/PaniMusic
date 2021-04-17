using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaniMusic.Core.Models;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteAlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;

        private readonly IFavoriteTrackCrud favoriteTrackCrud;

        private readonly IFavoriteAlbumCrud favoriteAlbumCrud;

        private readonly IFavoriteMusicVideoCrud favoriteMusicVideoCrud;

        private readonly IFeedbackCrud feedbackCrud;

        public IndexModel(UserManager<User> userManager
            , IFavoriteTrackCrud favoriteTrackCrud
            , IFavoriteAlbumCrud favoriteAlbumCrud
            , IFavoriteMusicVideoCrud favoriteMusicVideoCrud
            , IFeedbackCrud feedbackCrud)
        {
            this.userManager = userManager;

            this.favoriteTrackCrud = favoriteTrackCrud;

            this.favoriteAlbumCrud = favoriteAlbumCrud;

            this.favoriteMusicVideoCrud = favoriteMusicVideoCrud;

            this.feedbackCrud = feedbackCrud;
        }

        public int CountOfFavoriteTracks { get; set; }

        public int CountOfFavoriteAlbums { get; set; }

        public int CountOfFavoriteMusicVideos { get; set; }

        public int CountOfUserFeedbacks { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await userManager.GetUserAsync(User);

            var favoriteTracks = await favoriteTrackCrud.GetFavoriteTracks(currentUser.Id);

            CountOfFavoriteTracks = favoriteTracks.Count();

            var favoriteAlbums = await favoriteAlbumCrud.GetFavoriteAlbums(currentUser.Id);

            CountOfFavoriteAlbums = favoriteAlbums.Count();

            var favoriteMusicVideos = await favoriteMusicVideoCrud.GetFavoriteMusicVideos(currentUser.Id);

            CountOfFavoriteMusicVideos = favoriteMusicVideos.Count();

            var userFeedbacks = await feedbackCrud.UserFeedbacks(currentUser.Id);

            CountOfUserFeedbacks = userFeedbacks.Count;

            return Page();
        }
    }
}
