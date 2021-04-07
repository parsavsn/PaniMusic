using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.ApplicationServices.Crud.UserCrud;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Services.ApplicationServices.CountStatistics
{
    public class CounStatistics : ICountStatistics
    {
        private readonly ITrackCrud trackCrud;

        private readonly IAlbumCrud albumCrud;

        private readonly IMusicVideoCrud musicVideoCrud;

        private readonly IArtistCrud artistCrud;

        private readonly IUserCrud userCrud;

        private readonly IFeedbackCrud feedbackCrud;

        public CounStatistics(ITrackCrud trackCrud
            , IAlbumCrud albumCrud
            , IMusicVideoCrud musicVideoCrud
            , IArtistCrud artistCrud
            , IUserCrud userCrud
            , IFeedbackCrud feedbackCrud)
        {
            this.trackCrud = trackCrud;

            this.albumCrud = albumCrud;

            this.musicVideoCrud = musicVideoCrud;

            this.artistCrud = artistCrud;

            this.userCrud = userCrud;

            this.feedbackCrud = feedbackCrud;
        }

        public async Task<int> CountOfTracks()
        {
            var getAllTracks = await trackCrud.GetAllTracks();

            return getAllTracks.Count;
        }

        public async Task<int> CountOfAlbums()
        {
            var getAllAlbums = await albumCrud.GetAllAlbums();

            return getAllAlbums.Count;
        }

        public async Task<int> CountOfMusicVideos()
        {
            var getAllMusicVideos = await musicVideoCrud.GetAllMusicVideos();

            return getAllMusicVideos.Count;
        }

        public async Task<int> CountOfArtists()
        {
            var getAllArtists = await artistCrud.GetAllArtists();

            return getAllArtists.Count;
        }

        public async Task<int> CountOfUsers()
        {
            var getAllUsers = await userCrud.GetAllUsers();

            return getAllUsers.Count;
        }

        public async Task<int> CountOfFeedbacks()
        {
            var getAllFeedbacks = await feedbackCrud.GetAllFeedbacks();

            return getAllFeedbacks.Count;
        }
    }
}
