namespace SimpleApi.Service.DTOs.Orders;

public record OrderDetailDto(
    int Id,
    DateTime OrderDate,
    decimal Amount,
    int CustomerId,
    IEnumerable<OrderItemDto> Items
);
