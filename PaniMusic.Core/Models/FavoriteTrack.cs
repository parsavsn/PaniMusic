using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class FavoriteTrack : IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int TrackId { get; set; }

        public User User { get; set; }

        public Track Track { get; set; }
    }
}
