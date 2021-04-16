﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class FavoriteAlbum : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int AlbumId { get; set; }

        public User User { get; set; }

        public Album Album { get; set; }
    }
}
