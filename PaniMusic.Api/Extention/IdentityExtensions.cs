using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Api.Extention
{
    public static class IdentityExtensions
    {
        public static void AddAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PaniMusicDbContext>();
        }
    }
}
