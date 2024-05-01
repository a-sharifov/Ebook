namespace Domain.CartAggregate.Errors;

public static class CartItemErrors
{
    public static Error QuantityExceedsBookQuantity =>
        new("CartItem.QuantityExceedsBookQuantity", "The quantity of the cart item exceeds the quantity of the book.");

    public static Error NotFound =>
      new("CartItem.NotFound", "The item was not found.");
}