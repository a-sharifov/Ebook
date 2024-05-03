using Domain.CartAggregate.Errors;
using Domain.UserAggregate.ValueObjects;

namespace Domain.CartAggregate.ValueObjects;

public sealed class CartExpirationTime : ValueObject
{
    public DateTime Value { get; private set; }
    public const int DefaultMinutes = 1;

    private CartExpirationTime(DateTime value) => 
        Value = value;

    public static Result<CartExpirationTime> Create(DateTime expirationTime)
    {
        return new CartExpirationTime(expirationTime);
    }

    public static CartExpirationTime Default()
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(DefaultMinutes);
        return new CartExpirationTime(expirationTime);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator CartExpirationTime(DateTime expirationTime) =>
       Create(expirationTime).Value;

    public static implicit operator DateTime(CartExpirationTime expirationTime) =>
        expirationTime.Value;
}
