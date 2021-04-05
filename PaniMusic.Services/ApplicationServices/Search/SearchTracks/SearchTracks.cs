using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchTracks
{
    public class SearchTracks : ISearchTracks
    {
        private readonly IRepository<Track> trackRepository;

        public SearchTracks(IRepository<Track> trackRepository)
        {
            this.trackRepository = trackRepository;
        }

        public async Task<IEnumerable<Track>> SearchTrack(string trackName)
        {
            var searchTracks = await trackRepository.GetQuery()
                .Include(x => x.Style)
                .Include(x => x.Artist)
                .Where(x => x.Name.Contains(trackName) && x.AlbumId == null)
                .ToListAsync();

            if (searchTracks == null)
                return null;

            return searchTracks;
        }
    }
}
