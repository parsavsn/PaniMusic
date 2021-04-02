using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class GalleryCategory : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }

        public string TitleTag { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTag { get; set; }

        public List<GalleryImage> GalleryImages { get; set; }
    }
}
