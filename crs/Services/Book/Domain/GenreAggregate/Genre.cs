using Domain.BookAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;

namespace Domain.GenreAggregate;

public class Genre : AggregateRoot<GenreId>
{
    public GenreName GenreName { get; private set; }

    private readonly List<Book> _books;
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    private Genre(GenreId id, GenreName genreName)
    {
        Id = id;
        GenreName = genreName;
        _books = [];
    }

    public static Result<Genre> Create(GenreId id, GenreName genreName)
    {
        var genre = new Genre(id, genreName);

        //TODO: Add domain events

        return Result.Success(genre);
    }
}
