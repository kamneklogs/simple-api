using SimpleApi.Service.DTOs.Customers;

namespace SimpleApi.Service.Services;

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken ct);
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken ct);
}
