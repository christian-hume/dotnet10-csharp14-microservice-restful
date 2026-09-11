using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.DTOs;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class GetAllProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public GetAllProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Obtiene todos los productos.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        return Ok(products);
    }
}
