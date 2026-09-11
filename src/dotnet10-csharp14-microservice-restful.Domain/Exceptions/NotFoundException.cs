namespace dotnet10.csharp14.microservice.restful.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, object key)
        : base($"'{entityName}' con identificador '{key}' no fue encontrado.") { }
}
