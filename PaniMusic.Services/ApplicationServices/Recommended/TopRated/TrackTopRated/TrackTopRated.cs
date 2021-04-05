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

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.TrackTopRated
{
    public class TrackTopRated : ITrackTopRated
    {
        private readonly IRepository<Track> trackRepository;

        private readonly IMapper mapper;

        public TrackTopRated(IRepository<Track> trackRepository, IMapper mapper)
        {
            this.trackRepository = trackRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopRatedTracks(int numberOfItems)
        {
            var allTracks = await trackRepository.GetQuery()
                .Include(x => x.Artist)
                .Include(y => y.Feedbacks)
                .ToListAsync();

            var topRatedTracks = allTracks
                .OrderByDescending(x => x.Feedbacks.Select(z => z.Rate))
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topRatedTracks);
        }
    }
}
