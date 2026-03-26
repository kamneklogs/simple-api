namespace SimpleApi.Service.DTOs.Customers;

public record CustomerDto(
    int Id,
    string Fullname,
    string Email
);
