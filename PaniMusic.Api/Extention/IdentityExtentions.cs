using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using PaniMusic.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Api.Extention
{
    public static class IdentityExtentions
    {
        public static void AddAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PaniMusicDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  var secretkey = Encoding.UTF8.GetBytes("1234567890asdfgh");

                  var encryptionkey = Encoding.UTF8.GetBytes("qwsadfrewtyh4532");

                  var validationParameters = new TokenValidationParameters
                  {
                      RequireSignedTokens = true,

                      ValidateIssuerSigningKey = true,

                      IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                      RequireExpirationTime = true,

                      ValidateLifetime = true,

                      ValidateAudience = true,

                      ValidAudience = "",

                      ValidateIssuer = true,

                      ValidIssuer = "",

                      TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                  };

                  options.RequireHttpsMetadata = true;

                  options.SaveToken = true;

                  options.TokenValidationParameters = validationParameters;
              });

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

            services.AddAuthorization(option =>
            {
                option.AddPolicy("UserPanel", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(PaniClaims.UserPanel, true.ToString());
                });

                option.AddPolicy("AdminPanel", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(PaniClaims.AdminPanel, true.ToString());
                });

                option.AddPolicy("NewItem", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(PaniClaims.NewItem, true.ToString());
                });

                option.AddPolicy("EditItem", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(PaniClaims.EditItem, true.ToString());
                });

                option.AddPolicy("DeleteItem", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(PaniClaims.DeleteItem, true.ToString());
                });
            });
        }
    }
}
