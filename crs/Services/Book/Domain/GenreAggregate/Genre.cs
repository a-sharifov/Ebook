using Domain.BookAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;
using Domain.SharedKernel.Entities;

namespace Domain.GenreAggregate;

public class Genre : AggregateRoot<GenreId>
{
    public GenreName Name { get; private set; }
    public Image Image { get; private set; }
    private readonly List<Book> _books = [];
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Genre() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Genre(GenreId id, GenreName name, Image image)
    {
        Id = id;
        Name = name;
        Image = image;
    }

    public static Result<Genre> Create(GenreId id, GenreName name, Image image)
    {
        var genre = new Genre(id, name, image);

        //TODO: Add domain events

        return Result.Success(genre);
    }
}
