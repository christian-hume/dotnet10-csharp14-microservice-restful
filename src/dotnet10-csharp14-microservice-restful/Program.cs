using Microsoft.OpenApi.Models;
using dotnet10.csharp14.microservice.restful.API.Middleware;
using dotnet10.csharp14.microservice.restful.Application;
using dotnet10.csharp14.microservice.restful.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ---- Registro de servicios (Composition Root) ----
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Service API",
        Version = "v1",
        Description = "Microservicio de gestión de productos construido con .NET 10 y arquitectura por capas."
    });
});

// Capas de Application e Infrastructure registran sus propias dependencias
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ---- Pipeline HTTP ----
app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service API v1"));
}

app.UseHttpsRedirection();
app.UseCors("DefaultCorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// Necesario para pruebas de integración con WebApplicationFactory
public partial class Program { }
