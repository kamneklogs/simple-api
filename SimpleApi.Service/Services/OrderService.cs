using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Data.Entities;
using SimpleApi.Service.DTOs.Orders;

using SimpleApi.Service.Interfaces;

namespace SimpleApi.Service.Services;

public class OrderService(SimpleApiDbContext db, IValidator<CreateOrderDto> validator) : IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken ct)
    {
        return await db.Orders
            .Select(o => new OrderDto(o.Id, o.OrderDate, o.Amount, o.CustomerId))
            .ToListAsync(ct);
    }

    public async Task<OrderDetailDto?> GetOrderByIdAsync(int id, CancellationToken ct)
    {
        var order = await db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

        if (order is null) return null;

        var items = order.Items.Select(i => new OrderItemDto(
            i.ProductId,
            i.Product.Name,
            i.Product.Price,
            i.Quantity
        ));

        return new OrderDetailDto(order.Id, order.OrderDate, order.Amount, order.CustomerId, items);
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(dto, ct);

        var productIds = dto.Items.Select(i => i.ProductId).ToList();
        var products = await db.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, ct);

        var items = dto.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity
        }).ToList();

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderDate = DateTime.UtcNow,
            Amount = items.Sum(i => products[i.ProductId].Price * i.Quantity),
            Items = items
        };

        foreach (var item in items)
            products[item.ProductId].Stock -= item.Quantity;

        db.Orders.Add(order);

        await db.SaveChangesAsync(ct);

        return new OrderDto(order.Id, order.OrderDate, order.Amount, order.CustomerId);
    }
}
