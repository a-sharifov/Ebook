using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            languageId => languageId.Value,
            value => new LanguageId(value))
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
            name => name.Value,
            value => LanguageName.Create(value).Value)
            .HasMaxLength(LanguageName.MaxLength)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasConversion(
            code => code.Value,
            value => LanguageCode.Create(value).Value)
            .HasMaxLength(LanguageCode.MaxLength)
            .IsRequired();

        builder.HasMany(x => x.Books)
           .WithOne(x => x.Language)
           .OnDelete(DeleteBehavior.Cascade); 
    }
}
