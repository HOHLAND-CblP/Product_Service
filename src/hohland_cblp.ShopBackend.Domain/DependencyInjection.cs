using hohland_cblp.ShopBackend.Domain.Services;
using hohland_cblp.ShopBackend.Domain.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace hohland_cblp.ShopBackend.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}