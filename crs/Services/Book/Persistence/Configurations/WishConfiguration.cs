using Domain.UserAggregate.Ids;
using Domain.WishAggregate;
using Domain.WishAggregate.Entities;
using Domain.WishAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class WishConfiguration : IEntityTypeConfiguration<Wish>
{
    public void Configure(EntityTypeBuilder<Wish> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            wishId => wishId.Value,
            value => new WishId(value))
            .IsRequired();

        builder.Property(x => x.UserId)
         .HasConversion(
         userId => userId.Value,
         value => new UserId(value))
         .IsRequired();

        builder.HasMany(x => x.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
