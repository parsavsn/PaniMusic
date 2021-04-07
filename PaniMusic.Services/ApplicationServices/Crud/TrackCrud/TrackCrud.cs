using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Track.Add;
using PaniMusic.Services.Map.CrudDtos.Track.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.TrackCrud
{
    public class TrackCrud : ITrackCrud
    {
        private readonly IRepository<Track> trackRepository;

        private readonly IMapper mapper;

        public TrackCrud(IRepository<Track> trackRepository, IMapper mapper)
        {
            this.trackRepository = trackRepository;

            this.mapper = mapper;
        }

        public async Task AddTrack(AddTrackInput addTrackInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addTrackInput.MyCoverImage, addNewGuid);

            await UploadFile(addTrackInput.MyQuality128, addNewGuid);

            await UploadFile(addTrackInput.MyQuality320, addNewGuid);

            var newTrack = mapper.Map<Track>(addTrackInput);

            newTrack.CoverImage = addNewGuid + addTrackInput.MyCoverImage.FileName;

            if (addTrackInput.MyQuality128.Length > 0)
                newTrack.Quality128 = addNewGuid + addTrackInput.MyQuality128.FileName;

            if (addTrackInput.MyQuality320.Length > 0)
                newTrack.Quality320 = addNewGuid + addTrackInput.MyQuality320.FileName;

            newTrack.Visit = 0;

            newTrack.RecordDate = DateTime.Now;

            trackRepository.Insert(newTrack);

            await trackRepository.Save();
        }

        public async Task<Track> GetTrack(string link)
        {
            var getTrack = await trackRepository.GetQuery()
                .Include(track => track.Style)
                .Include(track => track.Artist)
                .Include(track => track.Album)
                .Where(x => x.AlbumId == null)
                .FirstOrDefaultAsync(track => track.Link == link);

            if (getTrack == null)
                return null;

            getTrack.Visit++;

            trackRepository.Update(getTrack);

            await trackRepository.Save();

            return getTrack;
        }

        public async Task<List<Track>> GetAllTracks()
        {
            var getallTracks = await trackRepository.GetQuery()
                .Include(tracks => tracks.Style)
                .Include(tracks => tracks.Artist)
                .Include(tracks => tracks.Album)
                .Where(x => x.AlbumId == null)
                .ToListAsync();
            
            return getallTracks;
        }

        public async Task UpdateTrack(UpdateTrackInput updateTrackInput)
        {
            var updateNewGuid = Guid.NewGuid().ToString();

            await UploadFile(updateTrackInput.MyCoverImage, updateNewGuid);

            await UploadFile(updateTrackInput.MyQuality128, updateNewGuid);

            await UploadFile(updateTrackInput.MyQuality320, updateNewGuid);

            var getTrack = await trackRepository.Get(updateTrackInput.Id);

            var changeTrack = ChangeForUpdate(getTrack, updateTrackInput);

            trackRepository.Update(changeTrack);

            await trackRepository.Save();
        }

        public async Task DeleteTrack(int id)
        {
            var getTrack = await trackRepository.Get(id);

            DeleteFile(getTrack.CoverImage);

            DeleteFile(getTrack.Quality128);

            DeleteFile(getTrack.Quality320);

            trackRepository.Delete(id);

            await trackRepository.Save();
        }

        public async Task<List<Track>> GetTracksForAlbum(int albumId)
        {
            var getTracks = await trackRepository.GetQuery()
                .Include(tracks => tracks.Style)
                .Include(tracks => tracks.Artist)
                .Include(tracks => tracks.Album)
                .Where(tracks => tracks.AlbumId == albumId)
                .ToListAsync();

            return getTracks;
        }

        public async Task<List<Track>> GetTracksForArtist(int artistId)
        {
            var getTracks = await trackRepository.GetQuery()
                .Include(tracks => tracks.Style)
                .Include(tracks => tracks.Artist)
                .Include(tracks => tracks.Album)
                .Where(tracks => tracks.ArtistId == artistId && tracks.AlbumId == null)
                .ToListAsync();

            return getTracks;
        }

        public async Task<List<Track>> GetTracksForStyle(int styleId)
        {
            var getTracks = await trackRepository.GetQuery()
                .Include(tracks => tracks.Style)
                .Include(tracks => tracks.Artist)
                .Include(tracks => tracks.Album)
                .Where(tracks => tracks.StyleId == styleId  && tracks.AlbumId == null)
                .ToListAsync();

            return getTracks;
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "track",
                myGuid + myFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
            }
        }

        private Track ChangeForUpdate(Track track, UpdateTrackInput input)
        {
            track.Name = input.Name;

            if (input.MyCoverImage?.Length > 0)
                track.CoverImage = input.MyCoverImage.FileName;

            if (input.MyQuality128?.Length > 0)
                track.Quality128 = input.MyQuality128.FileName;

            if (input.MyQuality320?.Length > 0)
                track.Quality320 = input.MyQuality320.FileName;

            track.Lyric = input.Lyric;

            track.Link = input.Link;

            track.TitleTag = input.TitleTag;

            track.MetaDescription = input.MetaDescription;

            track.MetaTag = input.MetaTag;

            track.StyleId = input.StyleId;

            track.ArtistId = input.ArtistId;

            if (input.AlbumId != null)
                track.AlbumId = input.AlbumId;

            return track;
        }

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/track",
                  fileName);

                File.Delete(filePath);
            }
        }
    }
}
