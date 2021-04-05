using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.AlbumTopRated
{
    public interface IAlbumTopRated
    {
        Task<List<RecommendedOutput>> TopRatedAlbums(int numberOfItems);
    }
}
