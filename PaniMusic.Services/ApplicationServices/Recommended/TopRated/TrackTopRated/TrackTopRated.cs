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
            var topRatedTracks = await trackRepository.GetQuery()
                .Include(x => x.Artist)
                .Include(x => x.Feedbacks)
                .Where(x => x.AlbumId == null && x.Feedbacks.All(y => y.IsAccept == true))
                .OrderByDescending(x => x.Feedbacks.Average(x => x.Rate))
                .Take(numberOfItems)
                .ToListAsync();

            return mapper.Map<List<RecommendedOutput>>(topRatedTracks);
        }
    }
}
