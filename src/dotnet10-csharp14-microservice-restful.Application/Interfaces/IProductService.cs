using dotnet10.csharp14.microservice.restful.Application.Common;
using dotnet10.csharp14.microservice.restful.Application.DTOs;

namespace dotnet10.csharp14.microservice.restful.Application.Interfaces;

/// <summary>
/// Contrato de la capa de servicios (lógica de aplicación) para Product.
/// Los Controllers dependen de esta interfaz, nunca del repositorio directamente.
/// </summary>
public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> AdjustStockAsync(Guid id, int quantity, CancellationToken cancellationToken = default);
}
