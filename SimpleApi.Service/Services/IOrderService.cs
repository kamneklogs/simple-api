using SimpleApi.Service.DTOs.Orders;

namespace SimpleApi.Service.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken ct);
}
