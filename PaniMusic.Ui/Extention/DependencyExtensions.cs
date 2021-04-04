using Microsoft.Extensions.DependencyInjection;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Ui.Extention
{
    public static class DependencyExtensions
    {
        public static void AddDependency(this IServiceCollection services)
        {
            AddRepositories(services);

            AddServices(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Album>, Repository<Album>>();

            services.AddTransient<IRepository<Artist>, Repository<Artist>>();

            services.AddTransient<IRepository<GalleryCategory>, Repository<GalleryCategory>>();

            services.AddTransient<IRepository<GalleryImage>, Repository<GalleryImage>>();

            services.AddTransient<IRepository<MusicVideo>, Repository<MusicVideo>>();

            services.AddTransient<IRepository<Style>, Repository<Style>>();

            services.AddTransient<IRepository<Track>, Repository<Track>>();
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAlbumCrud, AlbumCrud>();

            services.AddTransient<IArtistCrud, ArtistCrud>();

            services.AddTransient<IGalleryCategoryCrud, GalleryCategoryCrud>();

            services.AddTransient<IGalleryImageCrud, GalleryImageCrud>();

            services.AddTransient<IMusicVideoCrud, MusicVideoCrud>();

            services.AddTransient<IStyleCrud, StyleCrud>();

            services.AddTransient<ITrackCrud, TrackCrud>();
        }
    }
}
