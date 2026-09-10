using dotnet10.csharp14.microservice.restful.Domain.Common;

namespace dotnet10.csharp14.microservice.restful.Domain.Entities;

/// <summary>
/// Entidad de dominio Product.
/// </summary>
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Sku { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public void UpdateStock(int quantity)
    {
        if (Stock + quantity < 0)
            throw new InvalidOperationException("El stock no puede ser negativo.");

        Stock += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
