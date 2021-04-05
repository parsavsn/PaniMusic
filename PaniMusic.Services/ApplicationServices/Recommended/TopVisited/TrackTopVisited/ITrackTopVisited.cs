using PaniMusic.Services.Map.RecommendedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Recommended.TopVisited.TrackTopVisited
{
    public interface ITrackTopVisited
    {
        Task<List<RecommendedOutput>> TopVisitedTracks(int numberOfItems);
    }
}
