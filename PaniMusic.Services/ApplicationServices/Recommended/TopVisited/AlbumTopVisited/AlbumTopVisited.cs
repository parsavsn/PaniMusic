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

namespace PaniMusic.Services.ApplicationServices.Recommended.TopVisited.AlbumTopVisited
{
    public class AlbumTopVisited : IAlbumTopVisited
    {
        private readonly IRepository<Album> albumRepository;

        private readonly IMapper mapper;

        public AlbumTopVisited(IRepository<Album> albumRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;

            this.mapper = mapper;
        }

        public async Task<List<RecommendedOutput>> TopVisitedAlbums(int numberOfItems)
        {
            var allAlbums = await albumRepository.GetQuery()
                .Include(x => x.Artist)
                .ToListAsync();

            var topVisitedAlbums = allAlbums
                .OrderByDescending(x => x.Visit)
                .Take(numberOfItems)
                .ToList();

            return mapper.Map<List<RecommendedOutput>>(topVisitedAlbums);
        }
    }
}
