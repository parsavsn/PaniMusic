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

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.MusicVideoTopRated
{
    public class MusicVideoTopRated : IMusicVideoTopRated
    {
        private readonly IRepository<MusicVideo> musicVideoRepository;

        private readonly IMapper mapper;

        public MusicVideoTopRated(IRepository<MusicVideo> musicVideoRepository, IMapper mapper)
        {
            this.musicVideoRepository = musicVideoRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopRatedMusicVideos(int numberOfItems)
        {
            var allMusicVideos = await musicVideoRepository.GetQuery()
                .Include(x => x.Artist)
                .Include(x => x.Feedbacks)
                .ToListAsync();

            var topRatedMusicVideos = allMusicVideos
                .OrderByDescending(x => x.Feedbacks.Select(y => y.Rate))
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topRatedMusicVideos);
        }
    }
}
