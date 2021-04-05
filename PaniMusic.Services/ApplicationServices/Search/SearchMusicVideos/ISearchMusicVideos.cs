using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchMusicVideos
{
    public interface ISearchMusicVideos
    {
        Task<IEnumerable<MusicVideo>> SearchMusicVideo(string musicVideoName);
    }
}
