using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;
using PaniMusic.Services.Map.CrudDtos.Artist.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.ArtistCrud
{
    public interface IArtistCrud
    {
        Task<bool> AddArtist(AddArtistInput addArtistInput);

        Task<Artist> GetArtistByLink(string link);

        Task<Artist> GetArtistById(int id);

        Task<List<Artist>> GetAllArtists();

        Task<bool> UpdateArtist(UpdateArtistInput updateArtistInput);

        Task<bool> DeleteArtist(int id);
    }
}
