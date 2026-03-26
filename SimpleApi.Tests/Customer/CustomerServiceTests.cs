using FluentAssertions;
using FluentValidation;
using SimpleApi.Service.DTOs.Customers;
using SimpleApi.Service.Services;
using SimpleApi.Service.Validators;

namespace SimpleApi.Tests.Customer;

public class CustomerServiceTests(TestBaseFixture fixture) : IClassFixture<TestBaseFixture>
{
    private readonly CustomerService _sut = new(fixture.dbContext, new CreateCustomerValidator());

    [Fact (DisplayName = "Create a new customer and return the created customer details")]
    public async Task CreateCustomer()
    {
        // Arrange
        var dto = new CreateCustomerDto("Camilo Gutierrez", "camilo.gutierrez@gmail.com");

        // Act
        var result = await _sut.CreateCustomerAsync(dto, CancellationToken.None);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Fullname.Should().Be(dto.Fullname);
        result.Email.Should().Be(dto.Email);

        var createdCustomer = await fixture.dbContext.Customers.FindAsync(result.Id);
        createdCustomer.Should().NotBeNull();
        createdCustomer!.Fullname.Should().Be(dto.Fullname);
        createdCustomer.Email.Should().Be(dto.Email);
    }

    [Fact (DisplayName = "Create a customer with an invalid email should throw a validation exception")]
    public async Task CreateCustomer_InvalidEmail()
    {
        // Arrange
        var dto = new CreateCustomerDto("Camilo Gutierrez", "not-a-valid-email");

        // Act
        var act = async () => await _sut.CreateCustomerAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateCustomerValidator.InvalidEmailMsg}*");
    }
}
