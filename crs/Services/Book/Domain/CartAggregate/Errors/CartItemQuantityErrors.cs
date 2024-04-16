namespace Domain.CartAggregate.Errors;

public static class CartItemQuantityErrors
{
    public static Error QuantityCannotBeNegativeOrNull => 
        new("CartItem.QuantityCannotBeNegativeOrNull", "quantity cannot be negative or null");
}