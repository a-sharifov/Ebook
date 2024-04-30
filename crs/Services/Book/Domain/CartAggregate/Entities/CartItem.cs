using Domain.BookAggregate;
using Domain.BookAggregate.ValueObjects;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;

namespace Domain.CartAggregate.Entities;

//TODO: Add domain events
public class CartItem : Entity<CartItemId>
{
    public CartId CartId { get; private set; }
    public Book Book { get; private set; }
    public CartItemQuantity Quantity { get; private set; }
  
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CartItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private CartItem(CartItemId id, CartId cartId, Book book, CartItemQuantity quantity)
    {
        Id = id;
        CartId = cartId;
        Book = book;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(CartItemId id, CartId cartId, Book book, CartItemQuantity quantity)
    {
        var cartItem = new CartItem(id, cartId, book, quantity);
        return cartItem;
    }

    //change logic 
    public Result UpdateQuantity(CartItemQuantity quantity)
    {
        if (quantity.Value > Book.Quantity.Value + Quantity.Value)
        {
            return Result.Failure(
                CartItemErrors.QuantityExceedsBookQuantity);
        }

        var quantityBook = QuantityBook.Create(
            Quantity.Value + Book.Quantity.Value - quantity.Value).Value;

        Book.UpdateQuantity(quantityBook);
        Quantity = quantity;

        return Result.Success();
    }

    internal Result Increment()
    {
        var bookDecrementResult = Book.Decrement();

        if (bookDecrementResult.IsFailure)
        {
            return Result.Failure(
                bookDecrementResult.Error);
        }

        //TODO: fix
        var itemQuantityResult = CartItemQuantity.Create(Quantity.Value + 1);

        if (itemQuantityResult.IsFailure)
        {
            return Result.Failure(
                itemQuantityResult.Error);
        }

        Quantity = itemQuantityResult.Value;

        return Result.Success();
    }

    //add int operator convert
    public static implicit operator int(CartItem cartItem) =>
        cartItem.Quantity.Value;
}