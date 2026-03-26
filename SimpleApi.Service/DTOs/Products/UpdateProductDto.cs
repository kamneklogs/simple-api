namespace SimpleApi.Service.DTOs.Products;

public record UpdateProductDto(
    string Name,
    decimal Price,
    int Stock
);
