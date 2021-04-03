using AutoMapper;
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

            if (updateArtistInput.MyImage.Length > 0)
                await UploadFile(updateArtistInput.MyImage, updateNewGuid);

            var getArtist = await GetArtist(updateArtistInput.Link);

            var changeArtist = ChangeArtistForUpdate(getArtist, updateArtistInput);

            artistRepository.Update(changeArtist);

            await artistRepository.Save();
        }

        public async Task DeleteArtist(string link)
        {
            var getArtist = await GetArtist(link);

            artistRepository.Delete(getArtist.Id);

            await artistRepository.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
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
        private Artist ChangeArtistForUpdate(Artist artist, UpdateArtistInput updateArtistInput)
        {
            artist.Name = updateArtistInput.Name;

            if (updateArtistInput.MyImage.Length > 0)
                artist.Image = updateArtistInput.MyImage.FileName;

            artist.Biography = updateArtistInput.BioGraphy;

            artist.Link = updateArtistInput.Link;

            artist.TitleTag = updateArtistInput.TitleTag;

            artist.MetaDescription = updateArtistInput.MetaDescription;

            artist.MetaTag = updateArtistInput.MetaTag;

            return artist;
        }
    }
}
