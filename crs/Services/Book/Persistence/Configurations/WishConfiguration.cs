using Domain.UserAggregate.Entities;
using Domain.UserAggregate.Ids;
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

        builder.HasOne(x => x.User)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(x => x.Book)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
