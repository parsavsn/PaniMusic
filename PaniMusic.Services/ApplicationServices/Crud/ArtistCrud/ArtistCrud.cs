﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.Artist.Add;
using PaniMusic.Services.Map.CrudDtos.Artist.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.ArtistCrud
{
    public class ArtistCrud : IArtistCrud
    {
        private readonly IRepository<Artist> artistRepository;

        private readonly IMapper mapper;

        public ArtistCrud(IRepository<Artist> artistRepository, IMapper mapper)
        {
            this.artistRepository = artistRepository;

            this.mapper = mapper;
        }

        public async Task AddArtist(AddArtistInput addArtistInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addArtistInput.MyImage, addNewGuid);

            var newArtist = mapper.Map<Artist>(addArtistInput);

            newArtist.Image = addNewGuid + addArtistInput.MyImage.FileName;

            artistRepository.Insert(newArtist);

            await artistRepository.Save();
        }

        public async Task<Artist> GetArtist(string link)
        {
            var getArtist = await artistRepository.GetQuery()
                .FirstOrDefaultAsync(artist => artist.Link == link);

            if (getArtist == null)
                return null;

            return getArtist;
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            return await artistRepository.GetAll();
        }

        public async Task UpdateArtist(UpdateArtistInput updateArtistInput)
        {
            var updateNewGuid = Guid.NewGuid().ToString();

            await UploadFile(updateArtistInput.MyImage, updateNewGuid);

            var getArtist = await GetArtist(updateArtistInput.Link);

            var changeArtist = ChangeForUpdate(getArtist, updateArtistInput);

            artistRepository.Update(changeArtist);

            await artistRepository.Save();
        }

        public async Task DeleteArtist(string link)
        {
            var getArtist = await GetArtist(link);

            DeleteFile(getArtist.Image);

            artistRepository.Delete(getArtist.Id);

            await artistRepository.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "artist",
                myGuid + myFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
            }
        }

        private Artist ChangeForUpdate(Artist artist, UpdateArtistInput input)
        {
            artist.Name = input.Name;

            if (input.MyImage.Length > 0)
                artist.Image = input.MyImage.FileName;

            artist.Biography = input.BioGraphy;

            artist.Link = input.Link;

            artist.TitleTag = input.TitleTag;

            artist.MetaDescription = input.MetaDescription;

            artist.MetaTag = input.MetaTag;

            return artist;
        }

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/artist",
                  fileName);

                File.Delete(filePath);
            }
        }
    }
}
