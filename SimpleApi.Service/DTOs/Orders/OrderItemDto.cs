namespace SimpleApi.Service.DTOs.Orders;

public record OrderItemDto(
    int ProductId,
    string ProductName,
    decimal ProductPrice,
    int Quantity
);
