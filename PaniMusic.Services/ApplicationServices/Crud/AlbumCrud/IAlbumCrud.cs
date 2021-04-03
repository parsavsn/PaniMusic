using PaniMusic.Services.Map.CrudDtos.Album.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.AlbumCrud
{
    public interface IAlbumCrud
    {
        Task<string> AddAlbum(AddAlbumInput addAlbumInput);
    }
}
