using Domain.Core.Errors;

namespace Domain.OrderAggregate.Errors;

public static class OrderErrors
{
    public static Error OrderNotFound => new(
        "Order.NotFound",
        "Order with the specified ID was not found");

    public static Error OrderItemNotFound => new(
        "Order.ItemNotFound",
        "Order item with the specified ID was not found");

    public static Error OrderCannotBeCancelled => new(
        "Order.CannotBeCancelled",
        "Order cannot be cancelled because it has already been shipped or delivered");

    public static Error OrderCannotBeModified => new(
        "Order.CannotBeModified",
        "Order cannot be modified because it has already been processed, shipped, or delivered");
        
    public static Error OrderMustHaveItems => new(
        "Order.MustHaveItems",
        "Order must have at least one item");
}
