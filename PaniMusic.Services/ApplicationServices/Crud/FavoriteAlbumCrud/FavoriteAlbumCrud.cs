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
            var newFavoriteAlbum = mapper.Map<FavoriteAlbum>(input);

            favoriteAlbumRepository.Insert(newFavoriteAlbum);

            await favoriteAlbumRepository.Save();

            return true;
        }

        public async Task<List<Album>> GetFavoriteAlbums(string userId)
        {
            var getFavoriteAlbums = await favoriteAlbumRepository.GetQuery()
                .Include(favorite => favorite.Album)
                .Where(favorite => favorite.UserId == userId)
                .Select(favorite => favorite.Album)
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
