﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Users.Application.Interfaces;
using Products.Application.Interfaces;

namespace Users.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<Inno_ShopDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUserDbContext>(provider => provider.GetService<Inno_ShopDbContext>());
            services.AddScoped<IProductDbContext>(provider =>
               provider.GetService<Inno_ShopDbContext>());
            return services;
        }
    }
}