namespace Domain.CartAggregate.Errors;

public static class CartErrors
{
    public static Error ItemNotFound =>
        new("Cart.ItemNotFound", "The item was not found in the cart.");

    public static Error ItemAlreadyExists =>
        new("Cart.ItemAlreadyExists", "The item already exists in the cart.");

}