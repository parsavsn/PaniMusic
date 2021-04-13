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

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.AlbumTopRated
{
    public class AlbumTopRated : IAlbumTopRated
    {
        private readonly IRepository<Album> albumRepository;

        private readonly IMapper mapper;

        public AlbumTopRated(IRepository<Album> albumRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopRatedAlbums(int numberOfItems)
        {
            var topRatedAlbums = await albumRepository.GetQuery()
                .Include(x => x.Artist)
                .Include(x => x.Feedbacks)
                .OrderByDescending(x => x.Feedbacks.Average(y => y.Rate))
                .Take(numberOfItems)
                .ToListAsync();

            return mapper.Map<List<RecommendedOutput>>(topRatedAlbums);
        }
    }
}
