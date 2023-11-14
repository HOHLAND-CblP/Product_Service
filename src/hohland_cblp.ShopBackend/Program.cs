using System.Collections.Specialized;
using hohland_cblp.ShopBackend.Domain;
using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using hohland_cblp.ShopBackend.Domain.Services;
using hohland_cblp.ShopBackend.Domain.Services.Interfaces;
using hohland_cblp.ShopBackend.Infrastructure;
using hohland_cblp.ShopBackend.Infrastructure.GrpcServices;
using hohland_cblp.ShopBackend.Infrastructure.Interceptors;
using hohland_cblp.ShopBackend.Infrastructure.Repositories;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        NameValueCollection appSetings = System.Configuration.ConfigurationManager.AppSettings;
        var postgresConnectionString = appSetings.Get("PostgresConnectionString");
        if (String.IsNullOrEmpty(postgresConnectionString))
            throw new ArgumentNullException("PostgresConnectionString not set in app.settings");

        services.AddGrpc(options =>
            {
                options.Interceptors.Add<ErrorInterceptor>();
                options.Interceptors.Add<LogInterceptor>();
            })
            .AddJsonTranscoding();

        services.AddAutoMapper(typeof(Program));
        services.AddGrpcSwagger();

        
        services.AddControllers();
        services.AddDomain();
        services.AddInfrastructure(postgresConnectionString);
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