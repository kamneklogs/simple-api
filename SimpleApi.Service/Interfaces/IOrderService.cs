using SimpleApi.Service.DTOs.Orders;

namespace SimpleApi.Service.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken ct);
    Task<OrderDetailDto?> GetOrderByIdAsync(int id, CancellationToken ct);
    Task<OrderDto?> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct);
}
