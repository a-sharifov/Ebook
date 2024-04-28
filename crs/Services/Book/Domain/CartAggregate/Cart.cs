using Domain.BookAggregate.ValueObjects;
using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;
using Domain.UserAggregate.Ids;

namespace Domain.CartAggregate;

//TODO: Add domain events
public class Cart : AggregateRoot<CartId>
{
    public UserId UserId { get; private set; }
    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public DateTime? ExpirationTime { get; private set; }
    public decimal TotalPrice => CalculateTotalPrice();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Cart() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Cart(CartId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }

    public static Result<Cart> Create(CartId id, UserId userId)
    {
        var cart = new Cart(id, userId);
        return Result.Success(cart);
    }

    public Result AddItem(CartItem item)
    {
        var isExistInCart = _items.Exists(x => x.Book.Id == item.Book.Id);
        if (isExistInCart)
        {
            item = _items.First(x => x.Book.Id == item.Book.Id);
            var cartItemQuantityResult = CartItemQuantity.Create(item.Quantity.Value + 1);
            return cartItemQuantityResult.IsSuccess ?
                item.UpdateQuantity(cartItemQuantityResult.Value) : cartItemQuantityResult;
        }

        _items.Add(item);
        return Result.Success();
    }

    public Result RemoveItem(CartItemId itemId)
    {
        var cartItem = _items.SingleOrDefault(x => x.Id == itemId);
        if (cartItem is null)
        {
            return Result.Failure(CartErrors.ItemNotFound);
        }

        _items.Remove(cartItem);
        return Result.Success();
    }

    public Result UpdateItemQuantity(CartItemId itemId, CartItemQuantity itemQuantity)
    {
        var cartItem = _items.SingleOrDefault(x => x.Id == itemId);
        if (cartItem is null)
        {
            return Result.Failure(CartErrors.ItemNotFound);
        }

        return cartItem.UpdateQuantity(itemQuantity);
    }

    public void Clear()
    {
        foreach (var item in _items)
        {
            item.Book.UpdateQuantity(QuantityBook.Create
                (item.Quantity.Value + item.Book.Quantity.Value).Value);
        }
    }

    public decimal CalculateTotalPrice()
    {
        var totalPrice = 0m;
        foreach (var cartItem in _items)
        {
            totalPrice += cartItem.Book.Price * cartItem.Quantity.Value;
        }

        return totalPrice;
    }

}
