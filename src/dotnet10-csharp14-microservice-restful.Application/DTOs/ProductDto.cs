namespace dotnet10.csharp14.microservice.restful.Application.DTOs;

/// <summary>
/// DTO de salida: representa un producto hacia el cliente de la API.
/// </summary>
public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    string Sku,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
