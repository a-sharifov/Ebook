using Domain.AuthorAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.ValueObjects;
using Domain.GenreAggregate;

namespace Domain.BookAggregate;

public class Book : AggregateRoot<BookId>
{
    public Title Title { get; private set; }
    public BookDescription BookDescription { get; private set; }
    public List<Author> Authors { get; private set; }
    public PageCount PageCount { get; private set; }
    public Money Price { get; private set; }
    public List<ImageUrl> Images { get; private set; }
    public ISBN ISBN { get; private set; }
    public QuantityBook QuantityBook { get; private set; }
    public SoldUnits SoldUnits { get; private set; }
    public List<Genre> Genres { get; private set; }
}
