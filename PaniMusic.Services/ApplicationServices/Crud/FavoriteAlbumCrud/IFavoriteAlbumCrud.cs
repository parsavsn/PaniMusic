using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.FavoriteAlbum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteAlbumCrud
{
    public interface IFavoriteAlbumCrud
    {
        Task<bool> AddFavoriteAlbum(AddFavoriteAlbumInput input);

        Task<IEnumerable<FavoriteAlbum>> GetFavoriteAlbums(string userId);

        Task<bool> DeleteFavoriteAlbum(int favoriteAlbumId);
    }
}
