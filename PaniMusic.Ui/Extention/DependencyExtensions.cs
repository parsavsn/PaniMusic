using Microsoft.Extensions.DependencyInjection;
using PaniMusic.Core.Models;
using PaniMusic.Repository.ContextRepository;
using PaniMusic.Services.ApplicationServices.Crud.AlbumCrud;
using PaniMusic.Services.ApplicationServices.Crud.ArtistCrud;
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
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAlbumCrud, AlbumCrud>();

            services.AddTransient<IArtistCrud, ArtistCrud>();
        }
    }
}
