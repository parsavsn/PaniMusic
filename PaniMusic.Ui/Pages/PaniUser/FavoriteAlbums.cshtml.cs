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

namespace PaniMusic.Ui.Pages.PaniUser
{
    [Authorize(Policy = "UserPanel")]
    public class FavoriteAlbumsModel : PageModel
    {
        private readonly IFavoriteAlbumCrud favoriteAlbumCrud;

        private readonly UserManager<User> userManager;

        public FavoriteAlbumsModel(IFavoriteAlbumCrud favoriteAlbumCrud, UserManager<User> userManager)
        {
            this.favoriteAlbumCrud = favoriteAlbumCrud;

            this.userManager = userManager;
        }

        public IEnumerable<FavoriteAlbum> FavoriteAlbums { get; set; }

        public int PageId { get; set; }

        public double PageCount { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
        {
            var currentUser = await userManager.GetUserAsync(User);

            var favoriteAlbums = await favoriteAlbumCrud.GetFavoriteAlbums(currentUser.Id);

            int skip = (page - 1) * 10;

            int countOfFavorites = favoriteAlbums.Count();

            PageId = page;

            double countOfPages = (double)countOfFavorites / 10;

            PageCount = Math.Ceiling(countOfPages);

            FavoriteAlbums = favoriteAlbums.OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
