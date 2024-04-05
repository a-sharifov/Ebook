using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.SharedKernel.Entities;
using Domain.SharedKernel.ValueObjects;
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

        builder.Property(x => x.FirstName)
            .HasConversion(
            firstName => firstName.Value,
            value => FirstName.Create(value).Value)
            .HasMaxLength(FirstName.MaxLength)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(lastName => lastName.Value,
            value => LastName.Create(value).Value)
            .HasMaxLength(LastName.MaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasConversion(
            authorDescription => authorDescription.Value,
            value => AuthorDescription.Create(value).Value)
            .HasMaxLength(AuthorDescription.AuthorDescriptionMaxLength)
            .IsRequired();

        builder.HasMany(x => x.AuthorBooks)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Image)
            .WithOne()
            .HasPrincipalKey<Image>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
