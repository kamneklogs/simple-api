namespace SimpleApi.Service.DTOs.Customers;

public record CreateCustomerDto(
    string Fullname,
    string Email
);
