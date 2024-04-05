using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;
using Domain.LanguageAggregate.Ids;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);       

        builder.HasAlternateKey(x => x.ISBN);

        builder.Property(x => x.Id)
            .HasConversion(
            bookId => bookId.Value,
            value => new BookId(value))
            .IsRequired();

        builder.Property(x => x.Title)
            .HasConversion(
            title => title.Value,
            value => Title.Create(value).Value)
            .HasMaxLength(Title.TitleMaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasConversion(
            bookDescription => bookDescription.Value,
            value => BookDescription.Create(value).Value)
            .HasMaxLength(BookDescription.BookDescriptionMaxLength)
            .IsRequired();

        builder.Property(x => x.PageCount)
            .HasConversion(
            pageCount => pageCount.Value,
            value => PageCount.Create(value).Value)
            .IsRequired();

        builder.OwnsOne(x => x.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency)
            .IsRequired();

            priceBuilder.Property(m => m.Amount)
            .HasColumnType("decimal(18,2)").IsRequired();
        });

        builder.Property(x => x.LanguageId)
            .HasConversion(
            languageId => languageId.Value,
            value => new LanguageId(value))
            .IsRequired();

        builder.Property(x => x.ISBN)
            .HasConversion(
            isbn => isbn.Value,
            value => ISBN.Create(value).Value)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasConversion(
            quantityBook => quantityBook.Value,
            value => QuantityBook.Create(value).Value)
            .IsRequired();

        builder.Property(x => x.SoldUnits)
            .HasConversion(
            soldUnits => soldUnits.Value,
            value => SoldUnits.Create(value).Value)
            .IsRequired();

        builder.HasMany(x => x.Images)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Genres)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Authors)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
