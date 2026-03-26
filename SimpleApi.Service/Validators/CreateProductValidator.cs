using FluentValidation;
using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public const string EmptyNameMsg = "Product name is required.";
    public const string InvalidPriceMsg = "Price must be greater than zero.";
    public const string InvalidStockMsg = "Stock must be zero or greater.";

    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(EmptyNameMsg);

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage(InvalidPriceMsg);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage(InvalidStockMsg);
    }
}
