using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.Entities;

public class Wish : Entity<WishId>
{
    public BookId BookId { get; private set; }
    public UserId UserId { get; private set; }
}
