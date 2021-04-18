using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.FavoriteMusicVideo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud
{
    public interface IFavoriteMusicVideoCrud
    {
        Task<bool> AddFavoriteMusicVideo(AddFavoriteMusicVideoInput input);

        Task<FavoriteMusicVideo> GetFavoriteMusicVideo(int musicVideoId, string userId);

        Task<IEnumerable<FavoriteMusicVideo>> GetFavoriteMusicVideos(string userId);

        Task<bool> DeleteFavoriteMusicVideo(int favoriteMusicVideoId);
    }
}
