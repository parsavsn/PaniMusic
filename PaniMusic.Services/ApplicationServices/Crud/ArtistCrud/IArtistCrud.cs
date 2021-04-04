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
        Task AddArtist(AddArtistInput addArtistInput);

        Task<Artist> GetArtist(string link);

        Task<List<Artist>> GetAllArtists();

        Task UpdateArtist(UpdateArtistInput updateArtistInput);

        Task DeleteArtist(int id);
    }
}
