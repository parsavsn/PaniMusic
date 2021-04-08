﻿using PaniMusic.Repository.ContextRepository;
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
        public async Task<bool> AddAlbum(AddAlbumInput addAlbumInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addAlbumInput.MyCoverImage, addNewGuid);

            await UploadFile(addAlbumInput.MyQuality128, addNewGuid);

            await UploadFile(addAlbumInput.MyQuality320, addNewGuid);

            var newAlbum = mapper.Map<Album>(addAlbumInput);

            newAlbum.CoverImage = addNewGuid + addAlbumInput.MyCoverImage.FileName;

            if (addAlbumInput.MyQuality128.Length > 0)
                newAlbum.Quality128 = addNewGuid + addAlbumInput.MyQuality128.FileName;

            if (addAlbumInput.MyQuality320.Length > 0)
                newAlbum.Quality320 = addNewGuid + addAlbumInput.MyQuality320.FileName;

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
            var updateNewGuid = Guid.NewGuid().ToString();

            await UploadFile(updateAlbumInput.MyCoverImage, updateNewGuid);

            await UploadFile(updateAlbumInput.MyQuality128, updateNewGuid);

            await UploadFile(updateAlbumInput.MyQuality320, updateNewGuid);

            var getAlbum = await albumRepository.Get(updateAlbumInput.Id);

            var changeAlbum = ChangeForUpdate(getAlbum, updateAlbumInput);

            albumRepository.Update(changeAlbum);

            await albumRepository.Save();

            return true;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            var getAlbum = await albumRepository.Get(id);

            DeleteFile(getAlbum.CoverImage);

            DeleteFile(getAlbum.Quality128);

            DeleteFile(getAlbum.Quality320);

            albumRepository.Delete(id);

            await albumRepository.Save();

            return true;
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile?.Length > 0)
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
        }

        private Album ChangeForUpdate(Album album, UpdateAlbumInput input)
        {
            album.Name = input.Name;

            if (input.MyCoverImage?.Length > 0)
                album.CoverImage = input.MyCoverImage.FileName;

            if (input.MyQuality128?.Length > 0)
                album.Quality128 = input.MyQuality128.FileName;

            if (input.MyQuality320?.Length > 0)
                album.Quality320 = input.MyQuality320.FileName;

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
    }
}
