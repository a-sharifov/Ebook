using Domain.BookAggregate;
using Domain.WishAggregate.Ids;

namespace Domain.WishAggregate.Entities;

// if you need , you can made this class as an aggregate root
public class WishItem : Entity<WishItemId>
{
    public Book Book { get; private set; }
    public WishId WishId { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WishItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private WishItem(WishItemId id, Book book, WishId wishId)
    {
        Id = id;
        Book = book;
        WishId = wishId;
    }

    public static Result<WishItem> Create(WishItemId id, Book book, WishId wishId)
    {
        var wish = new WishItem(id, book, wishId);

        //TODO: Add domain events

        return wish;
    }
}
