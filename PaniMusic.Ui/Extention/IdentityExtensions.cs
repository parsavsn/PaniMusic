using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaniMusic.Ui.Extention
{
    public static class IdentityExtensions
    {
        public static void AddAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PaniMusicDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;
            });

        }
    }
}
