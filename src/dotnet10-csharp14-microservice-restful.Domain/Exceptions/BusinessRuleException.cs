namespace dotnet10.csharp14.microservice.restful.Domain.Exceptions;

/// <summary>
/// Excepción para violaciones de reglas de negocio del dominio.
/// </summary>
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}
