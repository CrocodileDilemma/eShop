﻿using Application.Data;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(options =>
            options.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(options =>
            options.GetRequiredService<ApplicationDbContext>());
       
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
