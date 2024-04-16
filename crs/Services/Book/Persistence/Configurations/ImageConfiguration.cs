using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Enumerations;
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
            value => ImageName.Create(value, false).Value)
            .HasMaxLength(ImageName.MaxLength)
            .IsRequired();

        builder.Property(x => x.Url)
            .HasConversion(
            name => name.Value,
            value => ImageUrl.Create(value).Value)
            .HasMaxLength(ImageUrl.MaxLength)
            .IsRequired();

        builder.Property(x => x.Bucket).HasConversion(
           bucket => bucket.Name,
           name => ImageBucket.FromName(name)
           ).IsRequired();
    }
}