using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs.Orders;
using SimpleApi.Service.Interfaces;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var order = await orderService.GetOrderByIdAsync(id, ct);
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
    {
        var order = await orderService.CreateOrderAsync(dto, ct);
        if (order is null) return NotFound();
        return CreatedAtAction(nameof(Create), new { id = order.Id }, order);
    }
}
