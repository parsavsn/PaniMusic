using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Album.Add;
using PaniMusic.Services.Map.CrudDtos.Album.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.AlbumCrud
{
    public interface IAlbumCrud
    {
        Task AddAlbum(AddAlbumInput addAlbumInput);

        Task<Album> GetAlbum(string link);

        Task<List<Album>> GetAllAlbums();

        Task UpdateAlbum(UpdateAlbumInput updateAlbumInput);

        Task DeleteTrack(string link);
    }
}
