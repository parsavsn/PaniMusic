using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopVisited.AlbumTopVisited
{
    public interface IAlbumTopVisited
    {
        Task<List<RecommendedOutput>> TopVisitedAlbums(int numberOfItems);
    }
}
