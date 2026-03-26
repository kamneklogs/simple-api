using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Service.DTOs.Orders;

namespace SimpleApi.Service.Services;

public class OrderService(SimpleApiDbContext db) : IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken ct)
    {
        return await db.Orders
            .Select(o => new OrderDto(o.Id, o.OrderDate, o.Amount, o.CustomerId))
            .ToListAsync(ct);
    }
}
