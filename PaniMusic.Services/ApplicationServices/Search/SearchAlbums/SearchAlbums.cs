using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchAlbums
{
    public class SearchAlbums : ISearchAlbums
    {
        private readonly IRepository<Album> albumRepository;

        public SearchAlbums(IRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task<IEnumerable<Album>> SearchAlbum(string albumName)
        {
            var searchAlbum = await albumRepository.GetQuery()
                .Include(x => x.Style)
                .Include(x => x.Artist)
                .Where(x => x.Name.Contains(albumName))
                .ToListAsync();

            return searchAlbum;
        }
    }
}
