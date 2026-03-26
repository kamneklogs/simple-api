using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Data.Entities;
using SimpleApi.Service.DTOs.Customers;

namespace SimpleApi.Service.Services;

public class CustomerService(SimpleApiDbContext db) : ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken ct)
    {
        var customer = new Customer
        {
            Fullname = dto.Fullname,
            Email = dto.Email
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync(ct);

        return new CustomerDto(customer.Id, customer.Fullname, customer.Email);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken ct)
    {
        return await db.Customers
            .Select(c => new CustomerDto(c.Id, c.Fullname, c.Email))
            .ToListAsync(ct);
    }
}
