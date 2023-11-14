using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using hohland_cblp.ShopBackend.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace hohland_cblp.ShopBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();

        return services;
    }
}