using FluentValidation;
using SimpleApi.Service.DTOs.Customers;

namespace SimpleApi.Service.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Fullname)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
