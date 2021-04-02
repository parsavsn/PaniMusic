﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class Style : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string TitleTag { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTag { get; set; }

        public List<Album> Albums { get; set; }

        public List<MusicVideo> MusicVideos { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
