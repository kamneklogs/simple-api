using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken ct);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken ct);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto, CancellationToken ct);
    Task<bool> DeleteProductAsync(int id, CancellationToken ct);
}
