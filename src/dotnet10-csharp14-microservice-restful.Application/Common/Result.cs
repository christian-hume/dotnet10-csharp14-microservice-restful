namespace dotnet10.csharp14.microservice.restful.Application.Common;

/// <summary>
/// Envoltorio genérico para representar el resultado de una operación
/// de negocio sin depender de excepciones para el flujo normal.
/// </summary>
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
