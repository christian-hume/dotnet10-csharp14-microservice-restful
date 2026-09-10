using Microsoft.Extensions.Logging;
using dotnet10.csharp14.microservice.restful.Application.Common;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;
using dotnet10.csharp14.microservice.restful.Domain.Entities;
using dotnet10.csharp14.microservice.restful.Domain.Interfaces;

namespace dotnet10.csharp14.microservice.restful.Application.Services;

/// <summary>
/// Implementación de la lógica de negocio/aplicación para Product.
/// Orquesta el repositorio y aplica reglas de negocio antes de persistir.
/// </summary>
public class ProductAppService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductAppService> _logger;
    private readonly IScriptService _scriptService;

    public ProductAppService(IProductRepository productRepository, ILogger<ProductAppService> logger, IScriptService scriptService)
    {
        _productRepository = productRepository;
        _logger = logger;
        _scriptService = scriptService;
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return product is null ? null : MapToDto(product);
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Select(MapToDto).ToList();
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var skuExists = await _productRepository.ExistsAsync(p => p.Sku == dto.Sku, cancellationToken);
        if (skuExists)
        {
            _logger.LogWarning("Intento de crear producto con SKU duplicado: {Sku}", dto.Sku);
            return Result<ProductDto>.Failure($"Ya existe un producto con el SKU '{dto.Sku}'.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            Sku = dto.Sku
        };

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Producto creado con Id {ProductId}", product.Id);

        // Delegar ejecución de tareas relacionadas a scripts a IScriptService
        _ = _scriptService.RunScriptAsync("post-create", new Dictionary<string, object?> { ["id"] = product.Id }, cancellationToken);

        return Result<ProductDto>.Success(MapToDto(product));
    }

    public async Task<Result<ProductDto>> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return Result<ProductDto>.Failure("Producto no encontrado.");

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result<ProductDto>.Success(MapToDto(product));
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return Result<bool>.Failure("Producto no encontrado.");

        product.Deactivate();
        product.IsDeleted = true;
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<ProductDto>> AdjustStockAsync(Guid id, int quantity, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return Result<ProductDto>.Failure("Producto no encontrado.");

        try
        {
            product.UpdateStock(quantity);
        }
        catch (InvalidOperationException ex)
        {
            return Result<ProductDto>.Failure(ex.Message);
        }

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Result<ProductDto>.Success(MapToDto(product));
    }

    private static ProductDto MapToDto(Product product) => new(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.Stock,
        product.Sku,
        product.IsActive,
        product.CreatedAt,
        product.UpdatedAt
    );
}
