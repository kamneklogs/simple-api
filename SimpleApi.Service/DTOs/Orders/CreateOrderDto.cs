namespace SimpleApi.Service.DTOs.Orders;

public record CreateOrderDto(
    int CustomerId,
    IEnumerable<CreateOrderItemDto> Items
);
