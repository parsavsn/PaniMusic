using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchMusicVideos
{
    public class SearchMusicVideos : ISearchMusicVideos
    {
        private readonly IRepository<MusicVideo> musicVideoRepository;

        public SearchMusicVideos(IRepository<MusicVideo> musicVideoRepository)
        {
            this.musicVideoRepository = musicVideoRepository;
        }

        public async Task<IEnumerable<MusicVideo>> SearchMusicVideo(string musicVideoName)
        {
            var searchMusicVideos = await musicVideoRepository.GetQuery()
                .Include(x => x.Style)
                .Include(x => x.Artist)
                .Where(x => x.Name.Contains(musicVideoName))
                .ToListAsync();

            return searchMusicVideos;
        }
    }
}
