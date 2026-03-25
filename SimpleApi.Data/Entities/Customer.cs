namespace SimpleApi.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = [];
}
