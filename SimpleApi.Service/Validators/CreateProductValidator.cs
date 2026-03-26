using FluentValidation;
using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Price)
            .GreaterThan(0);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0);
    }
}
