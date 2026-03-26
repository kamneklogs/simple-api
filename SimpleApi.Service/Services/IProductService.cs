using SimpleApi.Service.DTOs;

namespace SimpleApi.Service.Services;

public interface IProductService
{
    Task<int> CreateProductAsync(CreateProductDto dto);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}
