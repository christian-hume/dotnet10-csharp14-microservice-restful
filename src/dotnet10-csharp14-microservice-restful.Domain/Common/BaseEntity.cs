namespace dotnet10.csharp14.microservice.restful.Domain.Common;

/// <summary>
/// Clase base para todas las entidades del dominio.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
