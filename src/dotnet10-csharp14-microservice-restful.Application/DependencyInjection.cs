using Microsoft.Extensions.DependencyInjection;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;
using dotnet10.csharp14.microservice.restful.Application.Services;

namespace dotnet10.csharp14.microservice.restful.Application;

/// <summary>
/// Registro de dependencias de la capa Application.
/// Se invoca desde Program.cs (composition root) en la capa API.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductAppService>();
        return services;
    }
}
