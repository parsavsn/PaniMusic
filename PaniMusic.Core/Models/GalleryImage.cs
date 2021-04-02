using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class GalleryImage : IEntity
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public int GalleryCategoryId { get; set; }

        public GalleryCategory GalleryCategory { get; set; }
    }
}
