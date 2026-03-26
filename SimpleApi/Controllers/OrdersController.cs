using Microsoft.AspNetCore.Mvc;
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
}
