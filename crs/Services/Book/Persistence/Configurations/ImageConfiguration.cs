using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.ValueObjects;
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

        builder.Property(x => x.Name)
            .HasConversion(
            name => name.Value,
            value => ImageName.Create(value).Value)
            .HasMaxLength(ImageName.MaxLength)
            .IsRequired();

        builder.Property(x => x.BucketName)
            .HasConversion(
            bucketName => bucketName.Value,
            value => BucketName.Create(value).Value)
            .HasMaxLength(BucketName.MaxLength)
            .IsRequired();
    }
}