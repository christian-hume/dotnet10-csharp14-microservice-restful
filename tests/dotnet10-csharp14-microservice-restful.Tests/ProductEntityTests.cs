using System;
using dotnet10.csharp14.microservice.restful.Domain.Entities;
using Xunit;

namespace dotnet10.csharp14.microservice.restful.Tests;

public class ProductEntityTests
{
    [Fact]
    public void UpdateStock_DeberiaSumarStock_CuandoCantidadPositiva()
    {
        var product = new Product { Stock = 5 };

        product.UpdateStock(3);

        Assert.Equal(8, product.Stock);
        Assert.True(product.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void UpdateStock_DeberiaLanzar_CuandoResultadoNegativo()
    {
        var product = new Product { Stock = 2 };

        Assert.Throws<InvalidOperationException>(() => product.UpdateStock(-5));
    }

    [Fact]
    public void Deactivate_DeberiaMarcarInactivo()
    {
        var product = new Product { IsActive = true };

        product.Deactivate();

        Assert.False(product.IsActive);
        Assert.True(product.UpdatedAt <= DateTime.UtcNow);
    }
}
