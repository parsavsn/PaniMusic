﻿using Microsoft.Extensions.DependencyInjection;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
using PaniMusic.Services.ApplicationServices.Crud.FeedbackCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryCategoryCrud;
using PaniMusic.Services.ApplicationServices.Crud.GalleryImageCrud;
using PaniMusic.Services.ApplicationServices.Crud.MusicVideoCrud;
using PaniMusic.Services.ApplicationServices.Crud.StyleCrud;
using PaniMusic.Services.ApplicationServices.Crud.TrackCrud;
using PaniMusic.Services.ApplicationServices.NewsletterService;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.AlbumTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.MusicVideoTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopRated.TrackTopRated;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.AlbumTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.MusicVideoTopVisited;
using PaniMusic.Services.ApplicationServices.Recommended.TopVisited.TrackTopVisited;
using PaniMusic.Services.ApplicationServices.Search.SearchAlbums;
using PaniMusic.Services.ApplicationServices.Search.SearchMusicVideos;
using PaniMusic.Services.ApplicationServices.Search.SearchTracks;
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

            services.AddTransient<IRepository<Feedback>, Repository<Feedback>>();

            services.AddTransient<IRepository<GalleryCategory>, Repository<GalleryCategory>>();

            services.AddTransient<IRepository<GalleryImage>, Repository<GalleryImage>>();

            services.AddTransient<IRepository<MusicVideo>, Repository<MusicVideo>>();

            services.AddTransient<IRepository<Style>, Repository<Style>>();

            services.AddTransient<IRepository<Track>, Repository<Track>>();

            services.AddTransient<IRepository<Newsletter>, Repository<Newsletter>>();
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAlbumCrud, AlbumCrud>();

            services.AddTransient<IArtistCrud, ArtistCrud>();

            services.AddTransient<IFeedbackCrud, FeedbackCrud>();

            services.AddTransient<IGalleryCategoryCrud, GalleryCategoryCrud>();

            services.AddTransient<IGalleryImageCrud, GalleryImageCrud>();

            services.AddTransient<IMusicVideoCrud, MusicVideoCrud>();

            services.AddTransient<IStyleCrud, StyleCrud>();

            services.AddTransient<ITrackCrud, TrackCrud>();

            services.AddTransient<INewsletterService, NewsLetterService>();

            services.AddTransient<IAlbumTopRated, AlbumTopRated>();

            services.AddTransient<IMusicVideoTopRated, MusicVideoTopRated>();

            services.AddTransient<ITrackTopRated, TrackTopRated>();

            services.AddTransient<IAlbumTopVisited, AlbumTopVisited>();

            services.AddTransient<IMusicVideoTopVisited, MusicVideoTopVisited>();

            services.AddTransient<ITrackTopVisited, TrackTopVisited>();

            services.AddTransient<ISearchAlbums, SearchAlbums>();

            services.AddTransient<ISearchMusicVideos, SearchMusicVideos>();

            services.AddTransient<ISearchTracks, SearchTracks>();
        }
    }
}
