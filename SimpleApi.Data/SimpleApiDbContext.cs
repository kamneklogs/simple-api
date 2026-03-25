using Microsoft.EntityFrameworkCore;
using SimpleApi.Data.Entities;

namespace SimpleApi.Data;

public class SimpleApiDbContext(DbContextOptions<SimpleApiDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleApiDbContext).Assembly);
    }
}
