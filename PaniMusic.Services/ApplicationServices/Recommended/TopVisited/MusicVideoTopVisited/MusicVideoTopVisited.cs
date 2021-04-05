using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopVisited.MusicVideoTopVisited
{
    public class MusicVideoTopVisited : IMusicVideoTopVisited
    {
        private readonly IRepository<MusicVideo> musicVideoRepository;

        private readonly IMapper mapper;

        public MusicVideoTopVisited(IRepository<MusicVideo> musicVideoRepository, IMapper mapper)
        {
            this.musicVideoRepository = musicVideoRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopVisitedMusicVideos(int numberOfItems)
        {
            var allMusicVideos = await musicVideoRepository.GetQuery()
                .Include(x => x.Artist)
                .ToListAsync();

            var topVisitedMusicVideos = allMusicVideos
                .OrderByDescending(x => x.Visit)
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topVisitedMusicVideos);
        }
    }
}
