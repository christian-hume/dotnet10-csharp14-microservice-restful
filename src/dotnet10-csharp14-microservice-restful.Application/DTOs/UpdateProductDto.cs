namespace dotnet10.csharp14.microservice.restful.Application.DTOs;

/// <summary>
/// DTO de entrada para la actualización de un producto.
/// </summary>
public record UpdateProductDto(
    string Name,
    string? Description,
    decimal Price,
    int Stock
);
