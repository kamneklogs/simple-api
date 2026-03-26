namespace SimpleApi.Service.DTOs.Orders;

public record OrderDto(
    int Id,
    DateTime OrderDate,
    decimal Amount,
    int CustomerId
);
