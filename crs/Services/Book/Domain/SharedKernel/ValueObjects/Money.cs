namespace Domain.SharedKernel.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Value { get; private set; }

    private Money(decimal value) =>
        Value = value;

    public static Result<Money> Create(decimal currency)
    {
        if (currency <= 0)
        {
            return Result.Failure<Money>(
                MoneyErrors.CannotBeZeroOrNegative);
        }

        return new Money(currency);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Money money) => money.Value;
}
