using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApi.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public decimal Amount { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];
}

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.Amount).HasColumnType("decimal(18,2)");

        builder.HasOne(o => o.Customer)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
