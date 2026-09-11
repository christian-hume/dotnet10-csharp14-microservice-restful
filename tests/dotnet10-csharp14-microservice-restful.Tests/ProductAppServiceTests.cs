using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Services;
using dotnet10.csharp14.microservice.restful.Domain.Entities;
using dotnet10.csharp14.microservice.restful.Domain.Interfaces;
using Xunit;

namespace dotnet10.csharp14.microservice.restful.Tests;

public class ProductAppServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock = new();
    private readonly Mock<dotnet10.csharp14.microservice.restful.Application.Interfaces.IScriptService> _scriptServiceMock = new();
    private readonly ProductAppService _sut;

    public ProductAppServiceTests()
    {
        _sut = new ProductAppService(_repositoryMock.Object, NullLogger<ProductAppService>.Instance, _scriptServiceMock.Object);
    }

    [Fact]
    public async Task CreateAsync_DeberiaFallar_CuandoSkuYaExiste()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), default))
            .ReturnsAsync(true);

        var dto = new CreateProductDto("Teclado", "Mecánico", 50m, 10, "SKU-001");

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("SKU-001", result.Error);
    }

    [Fact]
    public async Task CreateAsync_DeberiaCrearProducto_CuandoSkuEsUnico()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), default))
            .ReturnsAsync(false);

        var dto = new CreateProductDto("Mouse", "Inalámbrico", 25m, 30, "SKU-002");

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Mouse", result.Value!.Name);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>(), default), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(default), Times.Once);
        _scriptServiceMock.Verify(s => s.RunScriptAsync("post-create", It.IsAny<System.Collections.Generic.IDictionary<string, object?>>(), default), Times.Once);
    }
}
