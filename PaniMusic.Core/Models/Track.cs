using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaniMusic.Core.Models
{
    public class Track : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoverImage { get; set; }

        public string Quality128 { get; set; }

        public string Quality320 { get; set; }

        public string Lyric { get; set; }

        public int Visit { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime RecordDate { get; set; }

        public string Link { get; set; }

        public string TitleTag { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTag { get; set; }

        public int StyleId { get; set; }

        public int ArtistId { get; set; }

        public int? AlbumId { get; set; }

        public List<Feedback> Feedbacks { get; set; }

        public Style Style { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }
    }
}
