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

        Task<FavoriteTrack> GetFavoriteTrack(int trackId, string userId);

        Task<IEnumerable<FavoriteTrack>> GetFavoriteTracks(string userId);

        Task<bool> DeleteFavoriteTrack(int favoriteTrackId);
    }
}
