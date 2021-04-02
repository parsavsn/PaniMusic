using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class Feedback : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public bool IsAccept { get; set; }

        public int? TrackId { get; set; }

        public int? AlbumId { get; set; }

        public int? MusicVideoId { get; set; }

        public string UserId { get; set; }

        public Track Track { get; set; }

        public Album Album { get; set; }

        public MusicVideo MusicVideo { get; set; }

        public User User { get; set; }
    }
}
