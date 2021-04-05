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

namespace PaniMusic.Services.ApplicationServices.Recommended.TopVisited.TrackTopVisited
{
    public class TrackTopVisited : ITrackTopVisited
    {
        private readonly IRepository<Track> trackRepository;

        private readonly IMapper mapper;

        public TrackTopVisited(IRepository<Track> trackRepository, IMapper mapper)
        {
            this.trackRepository = trackRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopVisitedTracks(int numberOfItems)
        {
            var allTracks = await trackRepository.GetQuery()
                .Include(x => x.Artist)
                .ToListAsync();

            var topVisitedTracks = allTracks
                .OrderByDescending(x => x.Visit)
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topVisitedTracks);
        }
    }
}
