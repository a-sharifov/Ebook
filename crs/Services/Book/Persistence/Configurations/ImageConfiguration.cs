using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Ids;
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


    }
}
