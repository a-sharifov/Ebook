using Domain.CartAggregate;
using Domain.CartAggregate.Ids;
using Domain.UserAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.HasMany(x => x.CartItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
