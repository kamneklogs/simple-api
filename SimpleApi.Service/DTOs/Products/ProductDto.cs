namespace SimpleApi.Service.DTOs.Products;

public record ProductDto(
    int Id,
    string Name,
    decimal Price,
    int Stock
);
