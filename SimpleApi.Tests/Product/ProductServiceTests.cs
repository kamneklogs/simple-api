using FluentAssertions;
using FluentValidation;
using SimpleApi.Service.DTOs.Products;
using SimpleApi.Service.Services;
using SimpleApi.Service.Validators;

namespace SimpleApi.Tests.Product;

public class ProductServiceTests(TestBaseFixture fixture) : IClassFixture<TestBaseFixture>
{
    private readonly ProductService _sut = new(fixture.dbContext, new CreateProductValidator());

    [Fact (DisplayName = "Create a product with valid data should succeed")]
    public async Task CreateProduct_HappyPath()
    {
        // Arrange
        var dto = new CreateProductDto("Laptop", 999.99m, 10);

        // Act
        var result = await _sut.CreateProductAsync(dto, CancellationToken.None);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(dto.Name);
        result.Price.Should().Be(dto.Price);
        result.Stock.Should().Be(dto.Stock);

        var persisted = await fixture.dbContext.Products.FindAsync(result.Id);
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be(dto.Name);
        persisted.Price.Should().Be(dto.Price);
        persisted.Stock.Should().Be(dto.Stock);
    }

    [Fact(DisplayName = "Creating a product with an empty name should throw a validation exception")]
    public async Task CreateProduct_EmptyName()
    {
        // Arrange
        var dto = new CreateProductDto(string.Empty, 999.99m, 10);

        // Act
        var act = async () => await _sut.CreateProductAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateProductValidator.EmptyNameMsg}*");
    }

    [Fact(DisplayName = "Creating a product with a zero price should throw a validation exception")]
    public async Task CreateProduct_ZeroPrice()
    {
        // Arrange
        var dto = new CreateProductDto("Laptop", 0m, 10);

        // Act
        var act = async () => await _sut.CreateProductAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateProductValidator.InvalidPriceMsg}*");
    }

    [Fact(DisplayName = "Creating a product with a negative stock should throw a validation exception")]
    public async Task CreateProduct_NegativeStock()
    {
        // Arrange
        var dto = new CreateProductDto("Laptop", 999.99m, -1);

        // Act
        var act = async () => await _sut.CreateProductAsync(dto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*{CreateProductValidator.InvalidStockMsg}*");
    }
}
