using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs.Products;
using SimpleApi.Service.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var products = await productService.GetAllProductsAsync(ct);
        return Ok(products);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await productService.DeleteProductAsync(id, ct);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto, CancellationToken ct)
    {
        var product = await productService.UpdateProductAsync(id, dto, ct);
        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken ct)
    {
        var product = await productService.CreateProductAsync(dto, ct);
        return CreatedAtAction(nameof(Create), product);
    }
}
