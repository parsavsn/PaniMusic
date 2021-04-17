using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.FavoriteTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteTrackCrud
{
    public class FavoriteTrackCrud : IFavoriteTrackCrud
    {
        private readonly IRepository<FavoriteTrack> favoriteTrackRepository;

        private readonly IMapper mapper;

        public FavoriteTrackCrud(IRepository<FavoriteTrack> favoriteTrackRepository, IMapper mapper)
        {
            this.favoriteTrackRepository = favoriteTrackRepository;

            this.mapper = mapper;
        }

        public async Task<bool> AddFavoriteTrack(AddFavoriteTrackInput input)
        {
            var newFavoriteTrack = mapper.Map<FavoriteTrack>(input);

            favoriteTrackRepository.Insert(newFavoriteTrack);

            await favoriteTrackRepository.Save();

            return true;
        }

        public async Task<IEnumerable<FavoriteTrack>> GetFavoriteTracks(string userId)
        {
            var getFavoriteTracks = await favoriteTrackRepository.GetQuery()
                .Include(favorite => favorite.Track)
                .Include(favorite => favorite.Track.Artist)
                .Include(favorite => favorite.Track.Style)
                .Where(favorite => favorite.UserId == userId)
                .ToListAsync();

            return getFavoriteTracks;
        }

        public async Task<bool> DeleteFavoriteTrack(int favoriteTrackId)
        {
            var getFavoriteTrack = await favoriteTrackRepository.Get(favoriteTrackId);

            if (getFavoriteTrack == null)
                return false;

            favoriteTrackRepository.Delete(favoriteTrackId);

            await favoriteTrackRepository.Save();

            return true;
        }        
    }
}
