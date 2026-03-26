using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Service.DTOs.Orders;

namespace SimpleApi.Service.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public const string CustomerNotFoundMsg = "Customer not found.";
    public const string EmptyItemsMsg = "Order must contain at least one item.";
    public const string ProductNotFoundMsg = "One or more products were not found.";
    public const string NotEnoughStockMsg = "One or more products do not have enough stock.";

    public CreateOrderValidator(SimpleApiDbContext db)
    {
        RuleFor(o => o.CustomerId)
            .GreaterThan(0)
            .MustAsync((id, ct) => db.Customers.AnyAsync(c => c.Id == id, ct))
            .WithMessage(CustomerNotFoundMsg);

        RuleFor(o => o.Items)
            .NotEmpty()
            .WithMessage(EmptyItemsMsg);

        RuleForEach(o => o.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).GreaterThan(0);
            item.RuleFor(i => i.Quantity).GreaterThan(0);
        });

        RuleFor(o => o.Items)
            .MustAsync(async (items, ct) =>
            {
                var productIds = items.Select(i => i.ProductId).Distinct().ToList();
                var foundCount = await db.Products.CountAsync(p => productIds.Contains(p.Id), ct);
                return foundCount == productIds.Count;
            })
            .WithMessage(ProductNotFoundMsg)
            .When(o => o.Items.Any());

        RuleFor(o => o.Items)
            .MustAsync(async (items, ct) =>
            {
                var productIds = items.Select(i => i.ProductId).Distinct().ToList();
                var products = await db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, ct);

                return items.All(i => products.TryGetValue(i.ProductId, out var product)
                                      && product.Stock >= i.Quantity);
            })
            .WithMessage(NotEnoughStockMsg)
            .When(o => o.Items.Any());
    }
}
