using Microsoft.Extensions.Configuration;
using System.Reflection;
using Users.Application;
using Users.Application.Common.Mappings;
using Users.Application.Interfaces;
using Users.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Users.WebApi.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Products.Application;
using Products.Application.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Products.Application.Services;

namespace Users.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IUserDbContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
            });

            services.AddApplication();
            services.AddApplicationProducts();
            services.AddDbContext<Inno_ShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddPersistence(Configuration);
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<Inno_ShopDbContext>()
            .AddDefaultTokenProviders();
            services.AddDataProtection()
           .PersistKeysToFileSystem(new DirectoryInfo(Configuration["PathKeys"]))
           .SetApplicationName("Inno_Shop");
            services.AddControllers();
            services.AddScoped<IProductService, ProductService>();
            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddAuthorization();
            services.AddTransient<IEmailService, EmailService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthentications();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}