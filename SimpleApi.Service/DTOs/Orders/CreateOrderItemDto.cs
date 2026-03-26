namespace SimpleApi.Service.DTOs.Orders;

public record CreateOrderItemDto(
    int ProductId,
    int Quantity
);
