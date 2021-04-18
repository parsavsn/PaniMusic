using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.FavoriteMusicVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteMusicVideoCrud
{
    public class FavoriteMusicVideoCrud : IFavoriteMusicVideoCrud
    {
        private readonly IRepository<FavoriteMusicVideo> favoriteMusicVideoRepository;

        private readonly IMapper mapper;

        public FavoriteMusicVideoCrud(IRepository<FavoriteMusicVideo> favoriteMusicVideoRepository, IMapper mapper)
        {
            this.favoriteMusicVideoRepository = favoriteMusicVideoRepository;

            this.mapper = mapper;
        }

        public async Task<bool> AddFavoriteMusicVideo(AddFavoriteMusicVideoInput input)
        {
            var hasMusicVideo = await favoriteMusicVideoRepository.GetQuery()
                .Where(favorite => favorite.UserId == input.UserId && favorite.MusicVideoId == input.MusicVideoId)
                .ToListAsync();

            if (hasMusicVideo.Count != 0)
                return false;

            var newFavoriteMusicVideo = mapper.Map<FavoriteMusicVideo>(input);

            favoriteMusicVideoRepository.Insert(newFavoriteMusicVideo);

            await favoriteMusicVideoRepository.Save();

            return true;
        }

        public async Task<FavoriteMusicVideo> GetFavoriteMusicVideo(int musicVideoId, string userId)
        {
            var getFavoriteMusicVideo = await favoriteMusicVideoRepository.GetQuery()
                .FirstOrDefaultAsync(favorite => favorite.UserId == userId && favorite.MusicVideoId == musicVideoId);

            if (getFavoriteMusicVideo == null)
                return null;

            return getFavoriteMusicVideo;
        }

        public async Task<IEnumerable<FavoriteMusicVideo>> GetFavoriteMusicVideos(string userId)
        {
            var getFavoriteMusicVideos = await favoriteMusicVideoRepository.GetQuery()
                .Include(favorite => favorite.MusicVideo)
                .Include(favorite => favorite.MusicVideo.Artist)
                .Include(favorite => favorite.MusicVideo.Style)
                .Where(favorite => favorite.UserId == userId)
                .ToListAsync();

            return getFavoriteMusicVideos;
        }

        public async Task<bool> DeleteFavoriteMusicVideo(int favoriteMusicVideoId)
        {
            var getFavoriteMusicVideo = await favoriteMusicVideoRepository.Get(favoriteMusicVideoId);

            if (getFavoriteMusicVideo == null)
                return false;

            favoriteMusicVideoRepository.Delete(favoriteMusicVideoId);

            await favoriteMusicVideoRepository.Save();

            return true;
        }
    }
}
