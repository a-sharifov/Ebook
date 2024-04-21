using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.Pseudonym);

        builder.Property(x => x.Id)
            .HasConversion(
            authorId => authorId.Value,
            value => new AuthorId(value))
            .IsRequired();

        builder.Property(x => x.Pseudonym)
            .HasConversion(
            pseudonym => pseudonym.Value,
            value => Pseudonym.Create(value).Value)
            .HasMaxLength(Pseudonym.MaxLength)
            .IsRequired();

        builder.HasMany(x => x.Books)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
