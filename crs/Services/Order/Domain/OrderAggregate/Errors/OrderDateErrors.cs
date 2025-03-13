using Domain.Core.Errors;

namespace Domain.OrderAggregate.Errors;

public static class OrderDateErrors
{
    public static Error CannotBeInFuture => new(
        "OrderDate.CannotBeInFuture",
        "Order date cannot be in the future");
        
    public static Error CannotBeEmpty => new(
        "OrderDate.CannotBeEmpty",
        "Order date cannot be empty");
}
