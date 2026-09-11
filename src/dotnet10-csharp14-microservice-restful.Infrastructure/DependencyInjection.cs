using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using dotnet10.csharp14.microservice.restful.Domain.Interfaces;
using dotnet10.csharp14.microservice.restful.Infrastructure.Persistence;
using dotnet10.csharp14.microservice.restful.Infrastructure.Repositories;

namespace dotnet10.csharp14.microservice.restful.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa Infrastructure (DbContext, repositorios).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, sql =>
                sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IProductRepository, ProductRepository>();
        // Servicios de infraestructura
        services.AddScoped<dotnet10.csharp14.microservice.restful.Application.Interfaces.IScriptService, dotnet10.csharp14.microservice.restful.Infrastructure.Services.ScriptService>();

        return services;
    }
}
