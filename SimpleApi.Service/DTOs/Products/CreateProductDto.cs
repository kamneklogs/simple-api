namespace SimpleApi.Service.DTOs.Products;

public record CreateProductDto(
    string Name,
    decimal Price,
    int Stock
);
