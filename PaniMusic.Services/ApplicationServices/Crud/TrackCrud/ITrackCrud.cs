using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.Track.Add;
using PaniMusic.Services.Map.CrudDtos.Track.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.TrackCrud
{
    public interface ITrackCrud
    {
        Task AddTrack(AddTrackInput addTrackInput);        

        Task<Track> GetTrack(string link);

        Task<List<Track>> GetAllTracks();

        Task UpdateTrack(UpdateTrackInput updateTrackInput);

        Task DeleteTrack(int id);

        // The bottom three methods are for display on ui pages .

        Task<List<Track>> GetTracksForAlbum(int albumId);

        Task<List<Track>> GetTracksForArtist(int artistId);

        Task<List<Track>> GetTracksForStyle(int styleId);
    }
}
