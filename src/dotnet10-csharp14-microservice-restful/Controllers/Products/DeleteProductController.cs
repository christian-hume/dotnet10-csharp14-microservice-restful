using Microsoft.AspNetCore.Mvc;
using dotnet10.csharp14.microservice.restful.Application.Interfaces;

namespace dotnet10.csharp14.microservice.restful.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class DeleteProductController : ControllerBase
{
    private readonly IProductService _productService;

    public DeleteProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Elimina (soft delete) un producto.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(id, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}
