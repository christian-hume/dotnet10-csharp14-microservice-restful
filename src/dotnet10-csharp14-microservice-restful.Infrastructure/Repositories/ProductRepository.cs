using Microsoft.EntityFrameworkCore;
using dotnet10.csharp14.microservice.restful.Domain.Entities;
using dotnet10.csharp14.microservice.restful.Domain.Interfaces;
using dotnet10.csharp14.microservice.restful.Infrastructure.Persistence;

namespace dotnet10.csharp14.microservice.restful.Infrastructure.Repositories;

/// <summary>
/// Repositorio específico para Product. Hereda las operaciones CRUD genéricas
/// y agrega consultas propias del dominio.
/// </summary>
public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(p => p.Sku == sku, cancellationToken);

    public async Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(p => p.IsActive).ToListAsync(cancellationToken);
}
