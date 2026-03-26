namespace SimpleApi.Service.DTOs;

public record UpdateProductDto(
    string Name,
    decimal Price,
    int Stock
);
