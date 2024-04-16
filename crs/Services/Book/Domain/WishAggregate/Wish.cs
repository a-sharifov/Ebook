using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Entities;
using Domain.WishAggregate.Ids;

namespace Domain.WishAggregate;

public class Wish : AggregateRoot<WishId>
{
    public UserId UserId { get; private set; }
    private readonly List<WishItem> _items = [];
    public IReadOnlyCollection<WishItem> Items => _items.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Wish() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Wish(WishId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }

    public static Result<Wish> Create(WishId id, UserId userId)
    {
        var wish = new Wish(id, userId);

        // todo: Add domain event

        return wish;
    }

    public Result AddItem(WishItem wish)
    {
        if (_items.Any(w => w.Book == wish.Book))
        {
            return Result.Failure(
                UserErrors.BookIsAlreadyInWishList);
        }

        _items.Add(wish);

        return Result.Success();
    }

    public Result RemoveItem(BookId bookId)
    {
        var wish = _items.FirstOrDefault(w => w.Book.Id == bookId);

        if (wish is null)
        {
            return Result.Failure(
                UserErrors.BookIsNotInWishList);
        }

        _items.Remove(wish);

        return Result.Success();
    }
}
