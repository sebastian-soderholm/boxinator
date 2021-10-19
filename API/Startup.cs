using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Services;
using boxinator.Services.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace boxinator
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<BoxinatorDbContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetSection("localSQLBoxinatorDB").Value));

            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISettingsService, SettingsService>();


            //FirebaseApp.Create(new AppOptions
            //{
            //    Credential = GoogleCredential.FromFile("firebase-secret-file.json")
            //});

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/boxinator";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/boxinator",
                        ValidateAudience = true,
                        ValidAudience = "boxinator",
                        ValidateLifetime = true
                    };
                    /*
                    options.Events = new JwtBearerEvents
                    {

                        OnTokenValidated = async ctx =>
                        {
                            // 1. grabs the user id from firebase
                            var name = ctx.Principal.Claims.First(c => c.Type == "user_id").Value;

                            // Get userManager out of DI
                            var _userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                            // 2. retrieves the roles that the user has
                            User user = await _userManager.FindByNameAsync(name);
                            var userRoles = await _userManager.GetRolesAsync(user);

                            //3.  adds the role as a new claim 
                            ClaimsIdentity identity = ctx.Principal.Identity as ClaimsIdentity;
                            if (identity != null)
                            {
                                foreach (var role in userRoles)
                                {
                                    identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, role));
                                }
                            }

                        }

                    };*/
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boxinator", Version = "v1" });
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "boxinator v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
