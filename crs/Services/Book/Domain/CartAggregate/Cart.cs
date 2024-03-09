using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;

namespace Domain.CartAggregate;

//TODO: Add domain events
public class Cart : AggregateRoot<CartId>
{
    public UserId UserId { get; private set; }
    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

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

    public Result AddItem(CartItem cartItem)
    {
        var isExistInCart = _cartItems.Exists(x => x.Id == cartItem.Id);
        if (isExistInCart)
        {
            cartItem = _cartItems.Single(x => x.Id == cartItem.Id);
            var cartItemQuantityResult = CartItemQuantity.Create(cartItem.CartItemQuantity.Value + 1);
            return cartItemQuantityResult.IsSuccess ?
                cartItem.UpdateQuantity(cartItemQuantityResult.Value) : cartItemQuantityResult;
        }

        _cartItems.Add(cartItem);
        return Result.Success();
    }

    public Result RemoveItem(CartItemId cartItemId)
    {
        var cartItem = _cartItems.SingleOrDefault(x => x.Id == cartItemId);
        if (cartItem is null)
        {
            return Result.Failure(CartErrors.ItemNotFound);
        }

        _cartItems.Remove(cartItem);
        return Result.Success();
    }

    public Result UpdateItemQuantity(CartItemId cartItemId, CartItemQuantity cartItemQuantity)
    {
        var cartItem = _cartItems.SingleOrDefault(x => x.Id == cartItemId);
        if (cartItem is null)
        {
            return Result.Failure(CartErrors.ItemNotFound);
        }

        return cartItem.UpdateQuantity(cartItemQuantity);
    }

    public void Clear() => _cartItems.Clear();

    public decimal CalculateTotalPrice()
    {
        var totalPrice = 0m;
        foreach (var cartItem in _cartItems)
        {
            totalPrice += cartItem.Book.Price * cartItem.CartItemQuantity.Value;
        }

        return totalPrice;
    }

}
