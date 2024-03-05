using Domain.BookAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;

namespace Domain.GenreAggregate;

public class Genre : AggregateRoot<GenreId>
{
    public GenreName GenreName { get; private set; }
    public List<Book> Books { get; private set; }
}
