using Microsoft.AspNetCore.Mvc;
using SimpleApi.Service.DTOs.Customers;
using SimpleApi.Service.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        var customer = await customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(Create), customer);
    }
}
