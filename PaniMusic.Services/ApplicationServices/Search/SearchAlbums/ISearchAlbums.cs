using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Search.SearchAlbums
{
    public interface ISearchAlbums
    {
        Task<IEnumerable<Album>> SearchAlbum(string albumName); 
    }
}
