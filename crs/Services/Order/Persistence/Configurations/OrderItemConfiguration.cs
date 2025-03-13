namespace Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(
                id => id.Value,
                value => new OrderItemId(value));

        builder.Property(i => i.OrderId)
            .HasConversion(
                id => id.Value,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(i => i.BookId)
            .IsRequired();

        builder.Property(i => i.BookTitle)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.UnitPrice)
            .IsRequired();

        builder.OwnsOne(i => i.Quantity, q =>
        {
            q.Property(p => p.Value)
                .HasColumnName("Quantity")
                .IsRequired();
        });

    }
}
