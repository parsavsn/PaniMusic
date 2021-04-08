using PaniMusic.Core.Models;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Add;
using PaniMusic.Services.Map.CrudDtos.MusicVideo.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud
{
    public interface IMusicVideoCrud
    {
        Task<bool> AddMusicVideo(AddMusicVideoInput addMusicVideoInput);

        Task<MusicVideo> GetMusicVideoByLink(string link);

        Task<MusicVideo> GetMusicVideoById(int id);

        Task<List<MusicVideo>> GetAllMusicVideos();

        Task<bool> UpdateMusicVideo(UpdateMusicVideoInput updateMusicVideoInput);

        Task<bool> DeleteMusicVideo(int id);
    }
}
