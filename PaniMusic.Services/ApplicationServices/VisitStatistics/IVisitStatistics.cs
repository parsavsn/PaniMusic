using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.VisitStatistics
{
    public interface IVisitStatistics
    {
        Task<int> TrackVisitStatistics();

        Task<int> AlbumVisitStatistics();

        Task<int> MusicVideoStatistics();
    }
}
