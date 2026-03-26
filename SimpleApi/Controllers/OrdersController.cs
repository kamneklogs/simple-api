using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs.Orders;
using SimpleApi.Service.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var orders = await orderService.GetAllOrdersAsync(ct);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
    {
        var order = await orderService.CreateOrderAsync(dto, ct);
        if (order is null) return NotFound();
        return CreatedAtAction(nameof(Create), new { id = order.Id }, order);
    }
}
