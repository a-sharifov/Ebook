using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate;
using Domain.Common.ValueObjects;

namespace Domain.AuthorAggregate;

public class Author : AggregateRoot<AuthorId>
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public ImageUrl AuthorImage { get; private set; }
    public Pseudonym Pseudonym { get; private set; }
    public List<Book> AuhorBooks { get; private set; }
    public AuthorDescription AuthorDescription { get; private set; }
}
