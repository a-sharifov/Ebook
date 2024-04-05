using Domain.AuthorAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;
using Domain.SharedKernel.Entities;
using Domain.GenreAggregate;
using Domain.LanguageAggregate.Ids;

namespace Domain.BookAggregate;

public class Book : AggregateRoot<BookId>
{
    public Title Title { get; private set; }
    public BookDescription Description { get; private set; }
    public PageCount PageCount { get; private set; }
    public Money Price { get; private set; }
    public LanguageId LanguageId { get; private set; }
    public ISBN ISBN { get; private set; }
    public QuantityBook Quantity { get; private set; }
    public SoldUnits SoldUnits { get; private set; }

    private readonly List<Image> _images = [];
    public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

    private readonly List<Genre> _genres = [];
    public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();

    private readonly List<Author> _authors = [];
    public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Book() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Book(
        BookId id,
        Title title,
        BookDescription bookDescription,
        PageCount pageCount,
        Money price,
        LanguageId languageId,
        ISBN isbn,
        QuantityBook quantity,
        SoldUnits soldUnits,
        List<Image> images,
        List<Genre> genres,
        List<Author> authors)
    {
        Id = id;
        Title = title;
        Description = bookDescription;
        PageCount = pageCount;
        Price = price;
        LanguageId = languageId;
        ISBN = isbn;
        Quantity = quantity;
        SoldUnits = soldUnits;
        _images = images;
        _genres = genres;
        _authors = authors;
    }

    public static Result<Book> Create(
        BookId id,
        Title title,
        BookDescription description,
        PageCount pageCount,
        Money price,
        LanguageId languageId,
        ISBN isbn,
        QuantityBook quantity,
        SoldUnits soldUnits,
        List<Image> images,
        List<Genre> genres,
        List<Author> authors)
    {
        var book = new Book(
            id, title, description, pageCount, price, languageId, isbn, quantity, soldUnits, images, genres, authors);

        //TODO: Add domain events

        return book;
    }

}