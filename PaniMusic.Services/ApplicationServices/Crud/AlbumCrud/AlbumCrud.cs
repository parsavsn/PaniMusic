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

namespace PaniMusic.Services.ApplicationServices.Crud.AlbumCrud
{
    public class AlbumCrud : IAlbumCrud
    {
        private readonly IRepository<Album> albumRepository;

        private readonly IMapper mapper;

        public AlbumCrud(IRepository<Album> albumRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;

            this.mapper = mapper;
        }
        public async Task AddAlbum(AddAlbumInput addAlbumInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addAlbumInput.MyCoverImage, addNewGuid);

            await UploadFile(addAlbumInput.MyQuality128, addNewGuid);

            await UploadFile(addAlbumInput.MyQuality320, addNewGuid);

            var newAlbum = mapper.Map<Album>(addAlbumInput);

            newAlbum.CoverImage = addNewGuid + addAlbumInput.MyCoverImage.FileName;

            newAlbum.Quality128 = addNewGuid + addAlbumInput.MyQuality128.FileName;

            newAlbum.Qulity320 = addNewGuid + addAlbumInput.MyQuality320.FileName;

            newAlbum.Visit = 0;

            newAlbum.RecordDate = DateTime.Now;

            albumRepository.Insert(newAlbum);

            await albumRepository.Save();
        }

        public async Task<Album> GetAlbum(string link)
        {
            var getAlbum = await albumRepository.GetQuery()
                .Include(album => album.Style)
                .Include(album => album.Artist)
                .FirstOrDefaultAsync(album => album.Link == link);

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

        public async Task UpdateAlbum(UpdateAlbumInput updateAlbumInput)
        {
            var updateNewGuid = Guid.NewGuid().ToString();

            if (updateAlbumInput.MyCoverImage.Length > 0)
                await UploadFile(updateAlbumInput.MyCoverImage, updateNewGuid);

            if (updateAlbumInput.MyQuality128.Length > 0)
                await UploadFile(updateAlbumInput.MyQuality128, updateNewGuid);

            if (updateAlbumInput.MyQuality320.Length > 0)
                await UploadFile(updateAlbumInput.MyQuality320, updateNewGuid);

            var getAlbum = await GetAlbum(updateAlbumInput.Link);

            var changeAlbum = ChangeForUpdate(getAlbum, updateAlbumInput);

            albumRepository.Update(changeAlbum);

            await albumRepository.Save();
        }

        public async Task DeleteTrack(string link)
        {
            var getAlbum = await GetAlbum(link);

            var pathCoverImage = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploads/album",
                getAlbum.CoverImage);

            File.Delete(pathCoverImage);

            if (getAlbum.Quality128 != null)
            {
                var pathQuality128 = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/album",
                  getAlbum.Quality128);

                File.Delete(pathQuality128);
            }

            if (getAlbum.Qulity320 != null)
            {
                var pathQuality320 = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/album",
                  getAlbum.Qulity320);

                File.Delete(pathQuality320);
            }

            albumRepository.Delete(getAlbum.Id);

            await albumRepository.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "album",
                myGuid + myFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await myFile.CopyToAsync(stream);
            }
        }

        private Album ChangeForUpdate(Album album, UpdateAlbumInput input)
        {
            album.Name = input.Name;

            if(input.MyCoverImage.Length > 0)
                album.CoverImage = input.MyCoverImage.FileName;

            if(input.MyQuality128.Length > 0)
                album.Quality128 = input.MyQuality128.FileName;

            if(input.MyQuality320.Length > 0)
                album.Qulity320 = input.MyQuality320.FileName;

            album.Link = input.Link;

            album.TitleTag = input.TitleTag;

            album.MetaDescription = input.MetaDescription;

            album.MetaTag = input.MetaTag;

            album.StyleId = input.StyleId;

            album.ArtistId = input.ArtistId;

            return album;
        }
    }
}
