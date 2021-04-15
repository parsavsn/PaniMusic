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
            var newFavoriteMusicVideo = mapper.Map<FavoriteMusicVideo>(input);

            favoriteMusicVideoRepository.Insert(newFavoriteMusicVideo);

            await favoriteMusicVideoRepository.Save();

            return true;
        }

        public async Task<List<MusicVideo>> GetFavoriteMusicVideos(string userId)
        {
            var getFavoriteMusicVideos = await favoriteMusicVideoRepository.GetQuery()
                .Include(favorite => favorite.MusicVideo)
                .Where(favorite => favorite.UserId == userId)
                .Select(favorite => favorite.MusicVideo)
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
