namespace dotnet10.csharp14.microservice.restful.Application.DTOs;

/// <summary>
/// DTO de entrada para la creación de un producto.
/// </summary>
public record CreateProductDto(
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    string Sku
);
