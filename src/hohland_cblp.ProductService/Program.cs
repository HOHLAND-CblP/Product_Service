using System.Text.Json.Serialization;
using hohland_cblp.ProductService.Domain.RepositoryContracts;
using hohland_cblp.ProductService.Domain.Services;
using hohland_cblp.ProductService.Domain.Services.Interfaces;
using hohland_cblp.ProductService.Infrastructure.GrpcServices;
using hohland_cblp.ProductService.Infrastructure.Interceptors;
using hohland_cblp.ProductService.Infrastructure.Repositories;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddGrpc(options =>
            {
                options.Interceptors.Add<ErrorInterceptor>();
                options.Interceptors.Add<LogInterceptor>();
            })
            .AddJsonTranscoding();

        services.AddAutoMapper(typeof(Program));
        services.AddGrpcSwagger();

        
        services.AddControllers();
        services.AddScoped<IProductService, ProductService>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
        app.MapGrpcService<ProductGrpcService>();
        app.Run();
    }
}