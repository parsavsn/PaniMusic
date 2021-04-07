using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.CountStatistics
{
    public interface ICountStatistics
    {
        Task<int> CountOfTracks();

        Task<int> CountOfAlbums();

        Task<int> CountOfMusicVideos();

        Task<int> CountOfArtists();

        Task<int> CountOfUsers();

        Task<int> CountOfFeedbacks();
    }
}
