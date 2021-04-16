using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaniMusic.Core.DatabaseContext
{
    public class PaniMusicDbContext : IdentityDbContext<User>
    {
        public PaniMusicDbContext(DbContextOptions<PaniMusicDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=PaniMusicDB;Trusted_Connection=True");
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<GalleryCategory> GalleryCategories { get; set; }

        public DbSet<GalleryImage> GalleryImages { get; set; }

        public DbSet<MusicVideo> MusicVideos { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Newsletter> Newsletters { get; set; }

        public DbSet<FavoriteTrack> FavoriteTracks { get; set; }

        public DbSet<FavoriteAlbum> FavoriteAlbums { get; set; }

        public DbSet<FavoriteMusicVideo> FavoriteMusicVideos { get; set; }
    }
}
