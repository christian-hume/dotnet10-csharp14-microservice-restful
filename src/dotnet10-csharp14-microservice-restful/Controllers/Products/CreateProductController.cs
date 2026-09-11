using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class CreateProductController : ControllerBase
{
    private readonly IProductService _productService;

    public CreateProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Crea un nuevo producto.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateAsync(dto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(nameof(GetProductByIdController.GetById), "GetProductById", new { id = result.Value!.Id }, result.Value);
    }
}
