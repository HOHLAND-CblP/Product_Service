﻿using hohland_cblp.ShopBackend.Domain.Contracts.Repositories;
using hohland_cblp.ShopBackend.Infrastructure.Interceptors;
using hohland_cblp.ShopBackend.Infrastructure.PostgresInfrastructure;
using hohland_cblp.ShopBackend.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace hohland_cblp.ShopBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IProductRepository, ProductRepository>(_=>new ProductRepository(connectionString));
        
        services.AddGrpc(options =>
            {
                options.Interceptors.Add<ErrorInterceptor>();
                options.Interceptors.Add<LogInterceptor>();
            })
            .AddJsonTranscoding();
        
        Postgres.MapCompositeTypes();
        
        Postgres.AddMigrations(services, connectionString);

        return services;
    }
}