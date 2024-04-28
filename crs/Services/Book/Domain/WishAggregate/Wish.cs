using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Entities;
using Domain.WishAggregate.Errors;
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
        var isExist = _items.Any(w => w.Book.Id == wish.Book.Id);

        if (isExist)
        {
            return Result.Failure(
                WishErrors.ThisBookIsExist);
        }

        _items.Add(wish);

        return Result.Success();
    }

    public Result RemoveItem(BookId bookId)
    {
        var isExist = _items.Any(wish => wish.Book.Id == bookId);

        if (!isExist)
        {
            return Result.Failure(
               UserErrors.BookIsNotInWishList);
        }

        var wish = _items.First(w => w.Book.Id == bookId);
        _items.Remove(wish);

        return Result.Success();
    }
}
