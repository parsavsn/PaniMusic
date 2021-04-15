using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.FavoriteTrack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud
{
    public interface IFavoriteTrackCrud
    {
        Task<bool> AddFavoriteTrack(AddFavoriteTrackInput input);

        Task<List<Track>> GetFavoriteTracks(string userId);

        Task<bool> DeleteFavoriteTrack(int favoriteTrackId);
    }
}
