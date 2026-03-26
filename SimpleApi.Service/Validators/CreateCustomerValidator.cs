using FluentValidation;
using SimpleApi.Service.DTOs.Customers;

namespace SimpleApi.Service.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public const string InvalidEmailMsg = "Email is not valid.";
    public const string EmptyFullnameMsg = "Fullname is required.";

    public CreateCustomerValidator()
    {
        RuleFor(c => c.Fullname)
            .NotEmpty()
            .WithMessage(EmptyFullnameMsg);

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(InvalidEmailMsg);
    }
}
