using Domain.AuthorAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;
using Domain.SharedKernel.Entities;
using Domain.GenreAggregate;
using Domain.LanguageAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.GenreAggregate.Ids;

namespace Domain.BookAggregate;

public class Book : AggregateRoot<BookId>
{
    public Title Title { get; private set; }
    public BookDescription Description { get; private set; }
    public PageCount PageCount { get; private set; }
    public Money Price { get; private set; }
    public Language Language { get; private set; }
    public QuantityBook Quantity { get; private set; }
    public SoldUnits SoldUnits { get; private set; }
    public Author Author { get; private set; }
    public Image Poster { get; private set; }
    public Genre Genre { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Book() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Book(
        BookId id,
        Title title,
        BookDescription bookDescription,
        PageCount pageCount,
        Money price,
        Language language,
        QuantityBook quantity,
        SoldUnits soldUnits,
        Image poster,
        Author author,
        Genre genre)
    {
        Id = id;
        Title = title;
        Description = bookDescription;
        PageCount = pageCount;
        Price = price;
        Language = language;
        Quantity = quantity;
        SoldUnits = soldUnits;
        Author = author;
        Poster = poster;
        Genre = genre;
    }

    public static Result<Book> Create(
        BookId id,
        Title title,
        BookDescription description,
        PageCount pageCount,
        Money price,
        Language language,
        QuantityBook quantity,
        SoldUnits soldUnits,
        Author author,
        Image poster,
        Genre genre)
    {
        var book = new Book(
            id, title, description, pageCount, price, language, quantity, soldUnits, poster, author, genre);

        //TODO: Add domain events

        return book;
    }

    public void UpdateQuantity(QuantityBook quantity)
    {
        Quantity = quantity;
    }

}