using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs.Customers;
using SimpleApi.Service.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var customers = await customerService.GetAllCustomersAsync(ct);
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto, CancellationToken ct)
    {
        var customer = await customerService.CreateCustomerAsync(dto, ct);
        return CreatedAtAction(nameof(Create), customer);
    }
}
