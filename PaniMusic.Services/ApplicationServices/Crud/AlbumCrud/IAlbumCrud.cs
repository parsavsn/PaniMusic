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
        Task<bool> AddAlbum(AddAlbumInput addAlbumInput);

        Task<Album> GetAlbumByLink(string link);

        Task<Album> GetAlbumById(int id);

        Task<List<Album>> GetAllAlbums();

        Task<bool> UpdateAlbum(UpdateAlbumInput updateAlbumInput);

        Task<bool> DeleteTrack(int id);
    }
}
