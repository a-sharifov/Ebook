using Domain.WishAggregate.Entities;
using Domain.WishAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class WishItemConfiguration : IEntityTypeConfiguration<WishItem>
{
    public void Configure(EntityTypeBuilder<WishItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            wishItemId => wishItemId.Value,
            value => new WishItemId(value))
            .IsRequired();

        builder.Property(x => x.WishId)
           .HasConversion(
           wishId => wishId.Value,
           value => new WishId(value))
           .IsRequired();

        builder.HasOne(x => x.Book)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
