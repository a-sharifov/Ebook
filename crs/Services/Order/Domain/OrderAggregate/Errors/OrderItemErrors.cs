using Domain.Core.Errors;

namespace Domain.OrderAggregate.Errors;

public static class OrderItemErrors
{
    public static Error InvalidQuantity => new(
        "OrderItem.InvalidQuantity",
        "Order item quantity must be greater than zero");
        
    public static Error InvalidPrice => new(
        "OrderItem.InvalidPrice",
        "Order item price must be greater than zero");
        
    public static Error BookIdCannotBeEmpty => new(
        "OrderItem.BookIdCannotBeEmpty",
        "Order item book ID cannot be empty");
        
    public static Error BookTitleCannotBeEmpty => new(
        "OrderItem.BookTitleCannotBeEmpty",
        "Order item book title cannot be empty");
        
    public static Error QuantityMustBeGreaterThanZero => new(
        "OrderItem.QuantityMustBeGreaterThanZero",
        "Order item quantity must be greater than zero");
}
