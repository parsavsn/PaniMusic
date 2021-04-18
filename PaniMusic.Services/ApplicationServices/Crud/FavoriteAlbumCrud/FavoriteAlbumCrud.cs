using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.FavoriteAlbum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.FavoriteAlbumCrud
{
    public class FavoriteAlbumCrud : IFavoriteAlbumCrud
    {
        private readonly IRepository<FavoriteAlbum> favoriteAlbumRepository;

        private readonly IMapper mapper;

        public FavoriteAlbumCrud(IRepository<FavoriteAlbum> favoriteAlbumRepository, IMapper mapper)
        {
            this.favoriteAlbumRepository = favoriteAlbumRepository;

            this.mapper = mapper;
        }

        public async Task<bool> AddFavoriteAlbum(AddFavoriteAlbumInput input)
        {
            var hasAlbum = await favoriteAlbumRepository.GetQuery()
                .Where(favorite => favorite.UserId == input.UserId && favorite.AlbumId == input.AlbumId)
                .ToListAsync();

            if (hasAlbum.Count != 0)
                return false;

            var newFavoriteAlbum = mapper.Map<FavoriteAlbum>(input);

            favoriteAlbumRepository.Insert(newFavoriteAlbum);

            await favoriteAlbumRepository.Save();

            return true;
        }

        public async Task<FavoriteAlbum> GetFavoriteAlbum(int albumId, string userId)
        {
            var getFavoriteAlbum = await favoriteAlbumRepository.GetQuery()
                .FirstOrDefaultAsync(favorite => favorite.UserId == userId && favorite.AlbumId == albumId);

            if (getFavoriteAlbum == null)
                return null;

            return getFavoriteAlbum;
        }

        public async Task<IEnumerable<FavoriteAlbum>> GetFavoriteAlbums(string userId)
        {
            var getFavoriteAlbums = await favoriteAlbumRepository.GetQuery()
                .Include(favorite => favorite.Album)
                .Include(favorite => favorite.Album.Artist)
                .Include(favorite => favorite.Album.Style)
                .Where(favorite => favorite.UserId == userId)
                .ToListAsync();

            return getFavoriteAlbums;
        }

        public async Task<bool> DeleteFavoriteAlbum(int favoriteAlbumId)
        {
            var getFavoriteAlbum = await favoriteAlbumRepository.Get(favoriteAlbumId);

            if (getFavoriteAlbum == null)
                return false;

            favoriteAlbumRepository.Delete(favoriteAlbumId);

            await favoriteAlbumRepository.Save();

            return true;
        }
    }
}
