using System.Net;
using System.Text.Json;
using dotnet10.csharp14.microservice.restful.Domain.Exceptions;

namespace dotnet10.csharp14.microservice.restful.API.Middleware;

/// <summary>
/// Middleware transversal que centraliza el manejo de excepciones no controladas
/// y las traduce a respuestas HTTP consistentes (Problem Details simplificado).
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WriteResponse(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado procesando la solicitud {Path}", context.Request.Path);
            await WriteResponse(context, HttpStatusCode.InternalServerError, "Ocurrió un error interno inesperado.");
        }
    }

    private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var payload = JsonSerializer.Serialize(new
        {
            status = (int)statusCode,
            title = message,
            traceId = context.TraceIdentifier
        });

        await context.Response.WriteAsync(payload);
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();
}
