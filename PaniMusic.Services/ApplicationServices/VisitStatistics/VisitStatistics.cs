using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.VisitStatistics
{
    public class VisitStatistics : IVisitStatistics
    {
        private readonly IRepository<Track> trackRepository;

        private readonly IRepository<Album> albumRepository;

        private readonly IRepository<MusicVideo> musicVideoRepository;

        public VisitStatistics(IRepository<Track> trackRepository
            , IRepository<Album> albumRepository
            , IRepository<MusicVideo> musicVideoRepository)
        {
            this.trackRepository = trackRepository;

            this.albumRepository = albumRepository;

            this.musicVideoRepository = musicVideoRepository;
        }

        public async Task<int> TrackVisitStatistics()
        {
            var getallTracks = await trackRepository.GetQuery()
                .Where(x => x.AlbumId == null)
                .ToListAsync();

            return getallTracks.Select(x => x.Visit).Sum();
        }

        public async Task<int> AlbumVisitStatistics()
        {
            var getAllAlbums = await albumRepository.GetAll();

            return getAllAlbums.Select(x => x.Visit).Sum();
        }

        public async Task<int> MusicVideoStatistics()
        {
            var getAllMusicVideos = await musicVideoRepository.GetAll();

            return getAllMusicVideos.Select(x => x.Visit).Sum();
        }
    }
}
