using Domain.CartAggregate;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;
using Domain.UserAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configurations.Converters;

namespace Persistence.Configurations;

internal sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            cartId => cartId.Value,
            value => new CartId(value))
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasConversion(
            buyerId => buyerId.Value,
            value => new UserId(value))
            .IsRequired();

        builder.Property(x => x.ExpirationTime)
            .HasConversion<CartExpirationTimeConverter>();

        builder.HasMany(x => x.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
