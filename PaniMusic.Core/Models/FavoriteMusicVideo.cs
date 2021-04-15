using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class FavoriteMusicVideo : IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MusicVideoId { get; set; }

        public User User { get; set; }

        public MusicVideo MusicVideo { get; set; }
    }
}
