using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;
using Domain.Common.Entities;

namespace Domain.AuthorAggregate;

public class Author : AggregateRoot<AuthorId>
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Image AuthorImage { get; private set; }
    public Pseudonym Pseudonym { get; private set; }
    public AuthorDescription AuthorDescription { get; private set; }

    private readonly List<Book> _auhorBooks;
    public IReadOnlyCollection<Book> AuthorBooks => _auhorBooks.AsReadOnly();

    private Author(
        AuthorId id,
        FirstName firstName,
        LastName lastName,
        Image authorImage,
        Pseudonym pseudonym,
        AuthorDescription authorDescription)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        AuthorImage = authorImage;
        Pseudonym = pseudonym;
        AuthorDescription = authorDescription;
        _auhorBooks = [];
    }

    public static Result<Author> Create(
        AuthorId id,
        FirstName firstName,
        LastName lastName,
        Image authorImage,
        Pseudonym pseudonym,
        AuthorDescription authorDescription)
    {
        var author = new Author(id, firstName, lastName, authorImage, pseudonym, authorDescription);

        // TODO: Add domain events

        return author;
    }
}
