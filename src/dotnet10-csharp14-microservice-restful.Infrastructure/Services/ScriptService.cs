using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;
using dotnet10.csharp14.microservice.restful.Infrastructure.Persistence;

namespace dotnet10.csharp14.microservice.restful.Infrastructure.Services;

/// <summary>
/// Implementación simple de IScriptService que puede ejecutar scripts contra la base de datos.
/// Esta implementación es mínima y debe adaptarse a las necesidades reales (seguridad, parametrización, etc.).
/// </summary>
public class ScriptService : IScriptService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ScriptService> _logger;

    public ScriptService(AppDbContext dbContext, ILogger<ScriptService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ScriptResult> RunScriptAsync(string scriptName, IDictionary<string, object?>? parameters = null, CancellationToken ct = default)
    {
        _logger.LogInformation("Ejecutando script {ScriptName}", scriptName);

        // Ejemplo: manejar scripts conocidos por nombre de forma segura.
        if (string.Equals(scriptName, "post-create", System.StringComparison.OrdinalIgnoreCase))
        {
            // Por ejemplo, actualizar una tabla de auditoría o disparar un stored procedure.
            // Aquí dejamos una implementación mínima que no modifica datos.
            await Task.CompletedTask;
            return new ScriptResult(true, "post-create ejecutado (no-op en implementación de ejemplo)");
        }

        _logger.LogWarning("Script no reconocido: {ScriptName}", scriptName);
        return new ScriptResult(false, $"Script no reconocido: {scriptName}");
    }
}
