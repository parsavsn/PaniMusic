using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public List<Feedback> Feedbacks { get; set; }

        public List<FavoriteTrack> FavoriteTracks { get; set; }

        public List<FavoriteAlbum> FavoriteAlbums { get; set; }

        public List<FavoriteMusicVideo> FavoriteMusicVideos { get; set; }
    }
}
