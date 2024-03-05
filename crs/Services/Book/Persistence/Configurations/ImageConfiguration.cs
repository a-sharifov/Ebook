using Domain.Common.Entities;
using Domain.Common.Ids;
using Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            imageId => imageId.Value,
            value => new ImageId(value))
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasConversion(
            imageUrl => imageUrl.Value,
            value => ImageUrl.Create(value).Value)
            .HasMaxLength(ImageUrl.MaxLength)
            .IsRequired();
    }
}
