using Domain.GenreAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            genreId => genreId.Value,
            value => new GenreId(value))
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
            genreName => genreName.Value,
            value => GenreName.Create(value).Value)
            .HasMaxLength(GenreName.MaxLength)
            .IsRequired();

        builder.HasMany(x => x.Books)
            .WithOne(x => x.Genre)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
