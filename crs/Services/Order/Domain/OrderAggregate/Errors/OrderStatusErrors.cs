using Domain.Core.Errors;

namespace Domain.OrderAggregate.Errors;

public static class OrderStatusErrors
{
    public static Error InvalidStatus => new(
        "OrderStatus.InvalidStatus",
        "The provided order status is invalid");
        
    public static Error CannotTransitionFromCurrentStatus => new(
        "OrderStatus.CannotTransition",
        "Cannot transition from the current order status to the requested status");
        
    public static Error CannotBeEmpty => new(
        "OrderStatus.CannotBeEmpty",
        "Order status cannot be empty");
        
    public static Error CannotChangeStatusOfCancelledOrder => new(
        "OrderStatus.CannotChangeCancelled",
        "Cannot change status of a cancelled order");
        
    public static Error DeliveredOrdersCanOnlyBeReturned => new(
        "OrderStatus.DeliveredOrdersCanOnlyBeReturned",
        "Delivered orders can only be returned");
}
