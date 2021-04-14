using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Album.Add;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaniMusic.Core.Models;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Services.Map.CrudDtos.Album.Update;
using System.Linq;

namespace PaniMusic.Services.ApplicationServices.Crud.AlbumCrud
{
    public class AlbumCrud : IAlbumCrud
    {
        private readonly IRepository<Album> albumRepository;

        private readonly IRepository<Track> trackRepository;

        private readonly IMapper mapper;

        public AlbumCrud(IRepository<Album> albumRepository, IRepository<Track> trackRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;

            this.trackRepository = trackRepository;

            this.mapper = mapper;
        }
        public async Task<bool> AddAlbum(AddAlbumInput addAlbumInput)
        {
            await UploadFile(addAlbumInput.MyCoverImage);

            await UploadFile(addAlbumInput.MyQuality128);

            await UploadFile(addAlbumInput.MyQuality320);

            var newAlbum = mapper.Map<Album>(addAlbumInput);

            newAlbum.CoverImage = addAlbumInput.MyCoverImage.FileName;

            if (addAlbumInput.MyQuality128?.Length > 0)
                newAlbum.Quality128 = addAlbumInput.MyQuality128.FileName;

            if (addAlbumInput.MyQuality320?.Length > 0)
                newAlbum.Quality320 = addAlbumInput.MyQuality320.FileName;

            newAlbum.Visit = 0;

            newAlbum.RecordDate = DateTime.Now;

            albumRepository.Insert(newAlbum);

            await albumRepository.Save();

            return true;
        }

        public async Task<Album> GetAlbumByLink(string link)
        {
            var getAlbum = await albumRepository.GetQuery()
                .Include(album => album.Style)
                .Include(album => album.Artist)
                .Include(album => album.Tracks)
                .FirstOrDefaultAsync(album => album.Link == link);

            if (getAlbum == null)
                return null;

            getAlbum.Visit++;

            albumRepository.Update(getAlbum);

            await albumRepository.Save();

            return getAlbum;
        }

        public async Task<Album> GetAlbumById(int id)
        {
            var getAlbum = await albumRepository.GetQuery()
                .Include(album => album.Style)
                .Include(album => album.Artist)
                .FirstOrDefaultAsync(album => album.Id == id);

            if (getAlbum == null)
                return null;

            return getAlbum;
        }

        public async Task<List<Album>> GetAllAlbums()
        {
            var getAllAlbums = await albumRepository.GetQuery()
                .Include(albums => albums.Style)
                .Include(albums => albums.Artist)
                .ToListAsync();

            return getAllAlbums;
        }

        public async Task<bool> UpdateAlbum(UpdateAlbumInput updateAlbumInput)
        {
            var getAlbum = await albumRepository.Get(updateAlbumInput.Id);

            if (getAlbum == null)
                return false;

            var tracksOfAlbum = await trackRepository.GetQuery()
                .Where(tracks => tracks.AlbumId == updateAlbumInput.Id)
                .ToListAsync();

            foreach (var item in tracksOfAlbum)
            {
                item.StyleId = updateAlbumInput.StyleId;

                item.ArtistId = updateAlbumInput.ArtistId;

                trackRepository.Update(item);
            }

            await trackRepository.Save();

            await UploadFile(updateAlbumInput.MyCoverImage);

            await UploadFile(updateAlbumInput.MyQuality128);

            await UploadFile(updateAlbumInput.MyQuality320);

            var changeAlbum = ChangeForUpdate(getAlbum, updateAlbumInput);

            albumRepository.Update(changeAlbum);

            await albumRepository.Save();

            return true;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            var getAlbum = await albumRepository.Get(id);

            if (getAlbum == null)
                return false;

            var tracksOfAlbum = await trackRepository.GetQuery()
                .Where(tracks => tracks.AlbumId == id)
                .ToListAsync();

            foreach (var item in tracksOfAlbum)
            {
                DeleteTrack(item.Quality128);

                DeleteTrack(item.Quality320);

                trackRepository.Delete(item.Id);
            }

            await trackRepository.Save();

            DeleteFile(getAlbum.CoverImage);

            DeleteFile(getAlbum.Quality128);

            DeleteFile(getAlbum.Quality320);

            albumRepository.Delete(id);

            await albumRepository.Save();

            return true;
        }

        private async Task UploadFile(IFormFile myFile)
        {
            if (myFile?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "album",
                myFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
            }
        }

        private Album ChangeForUpdate(Album album, UpdateAlbumInput input)
        {
            album.Name = input.Name;

            if (input.MyCoverImage?.Length > 0)
            {
                if (album.CoverImage != null)
                    DeleteFile(album.CoverImage);

                album.CoverImage = input.MyCoverImage.FileName; 
            }


            if (input.MyQuality128?.Length > 0)
            {
                if (album.Quality128 != null)
                    DeleteFile(album.Quality128);

                album.Quality128 = input.MyQuality128.FileName;
            }

            if (input.MyQuality320?.Length > 0)
            {
                if (album.Quality320 != null)
                    DeleteFile(album.Quality320);

                album.Quality320 = input.MyQuality320.FileName;
            }
                

            album.Link = input.Link;

            album.TitleTag = input.TitleTag;

            album.MetaDescription = input.MetaDescription;

            album.MetaTag = input.MetaTag;

            album.StyleId = input.StyleId;

            album.ArtistId = input.ArtistId;

            return album;
        }

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/album",
                  fileName);

                File.Delete(filePath);
            }
        }

        private void DeleteTrack(string fileName)
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
