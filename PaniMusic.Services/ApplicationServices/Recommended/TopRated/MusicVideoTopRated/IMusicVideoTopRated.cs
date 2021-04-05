using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.MusicVideoTopRated
{
    public interface IMusicVideoTopRated
    {
        Task<List<RecommendedOutput>> TopRatedMusicVideos(int numberOfItems);
    }
}
