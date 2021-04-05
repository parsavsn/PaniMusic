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
            var allAlbums = await albumRepository.GetQuery()
                .Include(x => x.Artist)
                .Include(x => x.Feedbacks)
                .ToListAsync();

            var topRatedAlbums = allAlbums
                .OrderByDescending(x => x.Feedbacks.Select(y => y.Rate))
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topRatedAlbums);
        }
    }
}
