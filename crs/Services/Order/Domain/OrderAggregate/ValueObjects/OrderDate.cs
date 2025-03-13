using Domain.Core.ValueObjects;
using Domain.OrderAggregate.Errors;

namespace Domain.OrderAggregate.ValueObjects;

public sealed class OrderDate : ValueObject
{
    public DateTime Value { get; }

    private OrderDate(DateTime value)
    {
        Value = value;
    }

    public static Result<OrderDate> Create(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            return Result.Failure<OrderDate>(OrderDateErrors.CannotBeInFuture);
        }

        return Result.Success(new OrderDate(value));
    }

    public static OrderDate Now() => new(DateTime.UtcNow);

    public static implicit operator DateTime(OrderDate date) => date.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
