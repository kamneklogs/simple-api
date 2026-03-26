using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Data.Entities;
using SimpleApi.Service.DTOs;

namespace SimpleApi.Service.Services;

public class ProductService(SimpleApiDbContext db) : IProductService
{
    public async Task<int> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        return product.Id;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await db.Products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToListAsync();
    }
}
