using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;

namespace Domain.AuthorAggregate;

public class Author : AggregateRoot<AuthorId>
{
    public Pseudonym Pseudonym { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Author() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Author(
        AuthorId id,
        Pseudonym pseudonym)
    {
        Id = id;
        Pseudonym = pseudonym;
    }

    public static Result<Author> Create(
        AuthorId id,
        Pseudonym pseudonym)
    {
        var author = new Author(id, pseudonym);

        // TODO: Add domain events

        return author;
    }

    public Result Update(Pseudonym pseudonym)
    {
        Pseudonym = pseudonym;
        // TODO: Add domain events

        return Result.Success();
    }
}
