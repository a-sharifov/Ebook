using Domain.AuthorAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;
using Domain.SharedKernel.Entities;
using Domain.GenreAggregate;
using Domain.LanguageAggregate;

namespace Domain.BookAggregate;

public class Book : AggregateRoot<BookId>
{
    public Title Title { get; private set; }
    public BookDescription Description { get; private set; }
    public PageCount PageCount { get; private set; }
    public Money Price { get; private set; }
    public Language Language { get; private set; }
    public ISBN ISBN { get; private set; }
    public QuantityBook Quantity { get; private set; }
    public SoldUnits SoldUnits { get; private set; }
    public Author Author { get; private set; }
    public Image Poster { get; private set; }

    private readonly List<Genre> _genres = [];
    public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();

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
        ISBN isbn,
        QuantityBook quantity,
        SoldUnits soldUnits,
        Image poster,
        Author author,
        List<Genre> genres)
    {
        Id = id;
        Title = title;
        Description = bookDescription;
        PageCount = pageCount;
        Price = price;
        Language = language;
        ISBN = isbn;
        Quantity = quantity;
        SoldUnits = soldUnits;
        Author = author;
        Poster = poster;
        _genres = genres;
    }

    public static Result<Book> Create(
        BookId id,
        Title title,
        BookDescription description,
        PageCount pageCount,
        Money price,
        Language language,
        ISBN isbn,
        QuantityBook quantity,
        SoldUnits soldUnits,
        Author author,
        Image poster,
        List<Genre> genres)
    {
        var book = new Book(
            id, title, description, pageCount, price, language, isbn, quantity, soldUnits, poster, author, genres);

        //TODO: Add domain events

        return book;
    }

}