using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class GetProductByIdController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<GetProductByIdController> _logger;

    public GetProductByIdController(IProductService productService, ILogger<GetProductByIdController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>Obtiene un producto por su identificador.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            _logger.LogWarning("Producto {ProductId} no encontrado", id);
            return NotFound(new { message = $"Producto con id '{id}' no encontrado." });
        }

        return Ok(product);
    }
}
