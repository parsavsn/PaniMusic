using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.AlbumTrack.Add;
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
        Task<bool> AddTrack(AddTrackInput addTrackInput);

        Task<bool> AddAlbumTrack(AddAlbumTrackInput addAlbumTrackInput);

        Task<Track> GetTrackByLink(string link);
        
        Task<Track> GetTrackById(int id);

        Task<List<Track>> GetTracksForAlbum(int albumId);

        Task<List<Track>> GetAllTracks();

        Task<bool> UpdateTrack(UpdateTrackInput updateTrackInput);

        Task<bool> DeleteTrack(int id);
    }
}
