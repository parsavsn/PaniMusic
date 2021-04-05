using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopRated.TrackTopRated
{
    public interface ITrackTopRated
    {
        Task<List<RecommendedOutput>> TopRatedTracks(int numberOfItems);
    }
}
