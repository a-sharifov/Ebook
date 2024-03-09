using Domain.BookAggregate;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;

namespace Domain.CartAggregate.Entities;

//TODO: Add domain events
public class CartItem : Entity<CartItemId>
{
    public CartId CartId { get; private set; }
    public Book Book { get; private set; }
    public CartItemQuantity CartItemQuantity { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CartItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private CartItem(CartItemId id, CartId cartId, Book book, CartItemQuantity cartItemQuantity)
    {
        Id = id;
        CartId = cartId;
        Book = book;
        CartItemQuantity = cartItemQuantity;
    }

    public static Result<CartItem> Create(CartItemId id, CartId cartId, Book book, CartItemQuantity cartItemQuantity)
    {
        var cartItem = new CartItem(id, cartId, book, cartItemQuantity);
        return cartItem;
    }

    public Result UpdateQuantity(CartItemQuantity cartItemQuantity)
    {
        if (cartItemQuantity.Value > Book.QuantityBook.Value)
        {
            return Result.Failure(
                CartItemErrors.QuantityExceedsBookQuantity);
        }

        CartItemQuantity = cartItemQuantity;
        return Result.Success();
    }

    //add int operator convert
    public static implicit operator int(CartItem cartItem) =>
        cartItem.CartItemQuantity.Value;
}