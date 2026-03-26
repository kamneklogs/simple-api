using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Data.Entities;
using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Services;

public class ProductService(SimpleApiDbContext db) : IProductService
{
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        return new ProductDto(product.Id, product.Name, product.Price, product.Stock);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();

        return true;
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return null;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await db.SaveChangesAsync();

        return new ProductDto(product.Id, product.Name, product.Price, product.Stock);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await db.Products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToListAsync();
    }
}
