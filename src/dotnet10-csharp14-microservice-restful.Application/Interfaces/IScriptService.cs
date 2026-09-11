using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet10.csharp14.microservice.restful.Application.Interfaces;

public record ScriptResult(bool Success, string Message, IDictionary<string, object?>? Data = null);

public interface IScriptService
{
    /// <summary>
    /// Ejecuta un script identificado por nombre con parámetros opcionales.
    /// Implementaciones concretas pueden ejecutar SQL, llamar procedimientos almacenados o invocar procesos externos.
    /// </summary>
    Task<ScriptResult> RunScriptAsync(string scriptName, IDictionary<string, object?>? parameters = null, CancellationToken ct = default);
}
