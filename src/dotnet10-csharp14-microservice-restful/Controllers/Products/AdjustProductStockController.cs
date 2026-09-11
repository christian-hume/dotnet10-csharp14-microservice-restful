using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class AdjustProductStockController : ControllerBase
{
    private readonly IProductService _productService;

    public AdjustProductStockController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Ajusta el stock de un producto (positivo suma, negativo resta).</summary>
    [HttpPatch("{id:guid}/stock")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> AdjustStock(Guid id, [FromBody] int quantity, CancellationToken cancellationToken)
    {
        var result = await _productService.AdjustStockAsync(id, quantity, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(result.Value);
    }
}
