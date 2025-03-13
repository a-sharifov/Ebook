namespace Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                value => new OrderId(value));

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .HasConversion(
                date => date.Value,
                value => OrderDate.Now())
            .IsRequired();

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(a => a.Street).HasColumnName("ShippingStreet").IsRequired();
            sa.Property(a => a.City).HasColumnName("ShippingCity").IsRequired();
            sa.Property(a => a.State).HasColumnName("ShippingState").IsRequired();
            sa.Property(a => a.Country).HasColumnName("ShippingCountry").IsRequired();
            sa.Property(a => a.ZipCode).HasColumnName("ShippingZipCode").IsRequired();
        });

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
