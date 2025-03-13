using Domain.Core.ValueObjects;
using Domain.OrderAggregate.Errors;

namespace Domain.OrderAggregate.ValueObjects;

public sealed class OrderItemQuantity : ValueObject
{
    public int Value { get; }

    private OrderItemQuantity(int value)
    {
        Value = value;
    }

    public static Result<OrderItemQuantity> Create(int value)
    {
        if (value <= 0)
        {
            return Result.Failure<OrderItemQuantity>(OrderItemErrors.QuantityMustBeGreaterThanZero);
        }

        return Result.Success(new OrderItemQuantity(value));
    }

    public static implicit operator int(OrderItemQuantity quantity) => quantity.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
