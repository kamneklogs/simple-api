using FluentAssertions;
using FluentValidation;
using SimpleApi.Service.DTOs.Orders;
using SimpleApi.Service.Services;
using SimpleApi.Service.Validators;
using CustomerEntity = SimpleApi.Data.Entities.Customer;
using ProductEntity = SimpleApi.Data.Entities.Product;

namespace SimpleApi.Tests.Order;

public class OrderServiceTests(TestBaseFixture fixture) : IClassFixture<TestBaseFixture>
{
    private readonly OrderService _sut = new(fixture.dbContext, new CreateOrderValidator(fixture.dbContext));

    [Fact(DisplayName = "Create order successfully when input is valid")]
    public async Task CreateOrder_HappyPath()
    {
        // Arrange
        var customer = new CustomerEntity { Fullname = "Santiago Ramirez", Email = "santiago.ramirez@gmail.com" };
        var product = new ProductEntity { Name = "Laptop", Price = 1500m, Stock = 10 };
        fixture.dbContext.Customers.Add(customer);
        fixture.dbContext.Products.Add(product);
        await fixture.dbContext.SaveChangesAsync();

        var dto = new CreateOrderDto(customer.Id, [new CreateOrderItemDto(product.Id, 2)]);

        // Act
        var result = await _sut.CreateOrderAsync(dto, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.CustomerId.Should().Be(customer.Id);
        result.Amount.Should().Be(product.Price * 2);

        var updatedProduct = await fixture.dbContext.Products.FindAsync(product.Id);
        updatedProduct!.Stock.Should().Be(8);
    }

    [Fact(DisplayName = "Creating an order with a non-existent customer should throw a validation exception")]
    public async Task CreateOrder_CustomerNotFound()
    {
        // Arrange
        var dto = new CreateOrderDto(99999, [new CreateOrderItemDto(1, 1)]);

        // Act
        var act = async () => await _sut.CreateOrderAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateOrderValidator.CustomerNotFoundMsg}*");
    }

    [Fact(DisplayName = "Creating an order with empty items should throw a validation exception")]
    public async Task CreateOrder_EmptyItems()
    {
        // Arrange
        var customer = new CustomerEntity { Fullname = "Valentina Torres", Email = "valentina.torres@gmail.com" };
        fixture.dbContext.Customers.Add(customer);
        await fixture.dbContext.SaveChangesAsync();

        var dto = new CreateOrderDto(customer.Id, []);

        // Act
        var act = async () => await _sut.CreateOrderAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateOrderValidator.EmptyItemsMsg}*");
    }

    [Fact(DisplayName = "Creating an order with a non-existent product should throw a validation exception")]
    public async Task CreateOrder_ProductNotFound()
    {
        // Arrange
        var customer = new CustomerEntity { Fullname = "Daniela Herrera", Email = "daniela.herrera@gmail.com" };
        fixture.dbContext.Customers.Add(customer);
        await fixture.dbContext.SaveChangesAsync();

        var dto = new CreateOrderDto(customer.Id, [new CreateOrderItemDto(99999, 1)]);

        // Act
        var act = async () => await _sut.CreateOrderAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateOrderValidator.ProductNotFoundMsg}*");
    }

    [Fact(DisplayName = "Creating an order with insufficient stock should throw a validation exception")]
    public async Task CreateOrder_InsufficientStock()
    {
        // Arrange
        var customer = new CustomerEntity { Fullname = "Andres Morales", Email = "andres.morales@gmail.com" };
        var product = new ProductEntity { Name = "Mouse", Price = 29.99m, Stock = 2 };
        fixture.dbContext.Customers.Add(customer);
        fixture.dbContext.Products.Add(product);
        await fixture.dbContext.SaveChangesAsync();

        var dto = new CreateOrderDto(customer.Id, [new CreateOrderItemDto(product.Id, 10)]);

        // Act
        var act = async () => await _sut.CreateOrderAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateOrderValidator.NotEnoughStockMsg}*");
    }
}
