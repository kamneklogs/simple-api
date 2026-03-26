using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Services;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteProductAsync(int id);
}
