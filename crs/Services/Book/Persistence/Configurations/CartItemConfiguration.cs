using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            cartItemId => cartItemId.Value,
            value => new CartItemId(value))
            .IsRequired();

        builder.Property(x => x.CartId)
            .HasConversion(
            bookId => bookId.Value,
            value => new CartId(value))
            .IsRequired();

        builder.HasOne(x => x.Book)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
