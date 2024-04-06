using Domain.BookAggregate;
using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.Entities;

// if you need , you can made this class as an aggregate root
public class Wish : Entity<WishId>
{
    public Book Book { get; private set; }
    public User User { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Wish() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Wish(WishId id, Book book, User user)
    {
        Id = id;
        Book = book;
        User = user;
    }

    public static Wish Create(WishId id, Book book, User user)
    {
        var wish = new Wish(id, book, user);

        //TODO: Add domain events

        return wish;
    }
}
