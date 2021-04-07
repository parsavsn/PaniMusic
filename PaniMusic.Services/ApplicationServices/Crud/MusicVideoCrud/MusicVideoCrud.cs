using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Add;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud
{
    public class MusicVideoCrud : IMusicVideoCrud
    {
        private readonly IRepository<MusicVideo> musicVideoRepository;

        private readonly IMapper mapper;

        public MusicVideoCrud(IRepository<MusicVideo> musicVideoRepository, IMapper mapper)
        {
            this.musicVideoRepository = musicVideoRepository;

            this.mapper = mapper;
        }

        public async Task AddMusicVideo(AddMusicVideoInput addMusicVideoInput)
        {
            var addNewGuid = Guid.NewGuid().ToString();

            await UploadFile(addMusicVideoInput.MyCoverImage, addNewGuid);

            await UploadFile(addMusicVideoInput.MyQuality480, addNewGuid);

            await UploadFile(addMusicVideoInput.MyQuality720, addNewGuid);

            await UploadFile(addMusicVideoInput.MyQuality1080, addNewGuid);

            var newMusicVideo = mapper.Map<MusicVideo>(addMusicVideoInput);

            newMusicVideo.CoverImage = addMusicVideoInput.MyCoverImage.FileName;

            if (addMusicVideoInput.MyQuality480.Length > 0)
                newMusicVideo.Quality480 = addMusicVideoInput.MyQuality480.FileName;

            if (addMusicVideoInput.MyQuality720.Length > 0)
                newMusicVideo.Quality720 = addMusicVideoInput.MyQuality720.FileName;

            if (addMusicVideoInput.MyQuality1080.Length > 0)
                newMusicVideo.Quality1080 = addMusicVideoInput.MyQuality1080.FileName;

            newMusicVideo.Visit = 0;

            newMusicVideo.RecordDate = DateTime.Now;

            musicVideoRepository.Insert(newMusicVideo);

            await musicVideoRepository.Save();
        }

        public async Task<MusicVideo> GetMusicVideo(string link)
        {
            var getMusicVideo = await musicVideoRepository.GetQuery()
                .Include(musicVideo => musicVideo.Style)
                .Include(musicVideo => musicVideo.Artist)
                .FirstOrDefaultAsync(musicVideo => musicVideo.Link == link);

            if (getMusicVideo == null)
                return null;

            getMusicVideo.Visit++;

            musicVideoRepository.Update(getMusicVideo);

            await musicVideoRepository.Save();

            return getMusicVideo;
        }

        public async Task<List<MusicVideo>> GetAllMusicVideos()
        {
            var getAllMusicVideos = await musicVideoRepository.GetQuery()
                .Include(musicVideo => musicVideo.Style)
                .Include(musicVideo => musicVideo.Artist)
                .ToListAsync();

            return getAllMusicVideos;
        }

        public async Task UpdateMusicVideo(UpdateMusicVideoInput updateMusicVideoInput)
        {
            var updateNewGuid = Guid.NewGuid().ToString();

            await UploadFile(updateMusicVideoInput.MyCoverImage, updateNewGuid);

            await UploadFile(updateMusicVideoInput.MyQuality480, updateNewGuid);

            await UploadFile(updateMusicVideoInput.MyQuality720, updateNewGuid);

            await UploadFile(updateMusicVideoInput.MyQuality1080, updateNewGuid);

            var getMusicVideo = await musicVideoRepository.Get(updateMusicVideoInput.Id);

            var changeMusicVideo = ChangeForUpdate(getMusicVideo, updateMusicVideoInput);

            musicVideoRepository.Update(changeMusicVideo);

            await musicVideoRepository.Save();
        }

        public async Task DeleteMusicVideo(int id)
        {
            var getMusicVideo = await musicVideoRepository.Get(id);

            DeleteFile(getMusicVideo.CoverImage);

            DeleteFile(getMusicVideo.Quality480);

            DeleteFile(getMusicVideo.Quality720);

            DeleteFile(getMusicVideo.Quality1080);

            musicVideoRepository.Delete(id);

            await musicVideoRepository.Save();
        }

        private async Task UploadFile(IFormFile myFile, string myGuid)
        {
            if (myFile?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "musicvideo",
                myGuid + myFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
            }
        }

        private MusicVideo ChangeForUpdate(MusicVideo musicVideo, UpdateMusicVideoInput input)
        {
            musicVideo.Name = input.Name;

            if (input.MyCoverImage?.Length > 0)
                musicVideo.CoverImage = input.MyCoverImage.FileName;

            if (input.MyQuality480?.Length > 0)
                musicVideo.Quality480 = input.MyQuality480.FileName;

            if (input.MyQuality720?.Length > 0)
                musicVideo.Quality720 = input.MyQuality720.FileName;

            if (input.MyQuality1080?.Length > 0)
                musicVideo.Quality1080 = input.MyQuality1080.FileName;

            musicVideo.Lyric = input.Lyric;

            musicVideo.Link = input.Link;

            musicVideo.TitleTag = input.TitleTag;

            musicVideo.MetaDescription = input.MetaDescription;

            musicVideo.MetaTag = input.MetaTag;

            return musicVideo;
        }

        private void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot/uploads/musicvideo",
                  fileName);

                File.Delete(filePath);
            }
        }
    }
}
