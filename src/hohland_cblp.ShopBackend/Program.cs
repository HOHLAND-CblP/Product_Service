using FluentMigrator.Runner;
using System.Collections.Specialized;
using hohland_cblp.ShopBackend.Domain;
using hohland_cblp.ShopBackend.Infrastructure;
using hohland_cblp.ShopBackend.Infrastructure.GrpcServices;

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
        

        services.AddAutoMapper(typeof(Program));
        services.AddGrpcSwagger();

        
        services.AddControllers();
        services.AddDomain();
        services.AddInfrastructure(postgresConnectionString);
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();


        var app = builder.Build();

        if (args.Length > 0 && args[0].Equals("migrate", StringComparison.InvariantCultureIgnoreCase))
        {
            using var scope = app.Services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
            return;
        }
        
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