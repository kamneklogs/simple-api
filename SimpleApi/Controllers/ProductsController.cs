using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs;
using SimpleApi.Service.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var id = await productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id }, new { id });
    }
}
