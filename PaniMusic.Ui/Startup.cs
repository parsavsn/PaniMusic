using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Services.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PaniMusic.Ui.Extention;
using PaniMusic.Core.Models;

namespace PaniMusic.Ui
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PaniMusicDbContext>(dbContext =>
            { dbContext.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperConfig)));

            // I put the dependency services in the DependencyExtensions.cs file from Extention folder

            services.AddDependency();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PaniMusicDbContext>()
                .AddDefaultTokenProviders();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/my404page";
                    await next();
                }
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
