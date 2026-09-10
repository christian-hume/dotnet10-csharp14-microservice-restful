using dotnet10.csharp14.microservice.restful.Domain.Entities;

namespace dotnet10.csharp14.microservice.restful.Domain.Interfaces;

/// <summary>
/// Contrato específico de repositorio para Product.
/// Extiende el repositorio genérico y agrega operaciones propias del dominio.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default);
}
