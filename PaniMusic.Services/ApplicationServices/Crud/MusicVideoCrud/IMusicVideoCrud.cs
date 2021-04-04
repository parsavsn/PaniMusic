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
        Task AddMusicVideo(AddMusicVideoInput addMusicVideoInput);

        Task<MusicVideo> GetMusicVideo(string link);

        Task<List<MusicVideo>> GetAllMusicVideos();

        Task UpdateMusicVideo(UpdateMusicVideoInput updateMusicVideoInput);

        Task DeleteMusicVideo(int id);
    }
}
