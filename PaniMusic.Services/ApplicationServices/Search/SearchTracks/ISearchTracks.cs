using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchTracks
{
    public interface ISearchTracks
    {
        Task<IEnumerable<Track>> SearchTrack(string trackName);
    }
}
