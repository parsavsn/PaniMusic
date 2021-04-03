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
        public async Task<string> AddAlbum(AddAlbumInput addAlbumInput)
        {
            var myNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addAlbumInput.MyCoverImage, myNewGuid);

            await UploadFile(addAlbumInput.MyQuality128, myNewGuid);

            await UploadFile(addAlbumInput.MyQuality320, myNewGuid);

            var newAlbum = mapper.Map<Album>(addAlbumInput);

            newAlbum.CoverImage = myNewGuid + addAlbumInput.MyCoverImage.FileName;

            newAlbum.Quality128 = myNewGuid + addAlbumInput.MyQuality128.FileName;

            newAlbum.Qulity320 = myNewGuid + addAlbumInput.MyQuality320.FileName;

            newAlbum.Visit = 0;

            newAlbum.RecordDate = DateTime.Now;

            albumRepository.Insert(newAlbum);

            await albumRepository.Save();

            return "success";
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "album",
                myGuid + myFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await myFile.CopyToAsync(stream);
            }
        }
    }
}
