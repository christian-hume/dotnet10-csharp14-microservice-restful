using Microsoft.EntityFrameworkCore;
using dotnet10.csharp14.microservice.restful.Domain.Entities;

namespace dotnet10.csharp14.microservice.restful.Infrastructure.Persistence;

/// <summary>
/// Contexto de base de datos (EF Core). Punto único de acceso a datos.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Filtro global de borrado lógico (soft delete)
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}
