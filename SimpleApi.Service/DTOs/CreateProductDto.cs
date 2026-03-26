namespace SimpleApi.Service.DTOs;

public record CreateProductDto(
    string Name,
    decimal Price,
    int Stock
);
