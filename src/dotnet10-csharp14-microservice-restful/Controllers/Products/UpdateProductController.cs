using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class UpdateProductController : ControllerBase
{
    private readonly IProductService _productService;

    public UpdateProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Actualiza un producto existente.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(id, dto, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return Ok(result.Value);
    }
}
