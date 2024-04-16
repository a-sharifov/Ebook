using Domain.BookAggregate.Errors;

namespace Domain.BookAggregate.ValueObjects;

public class SoldUnits : ValueObject
{
    public int Value { get; private set; }

    private SoldUnits(int value) => Value = value;

    public static Result<SoldUnits> Create(int soldUnits)
    {
        if (soldUnits < 0)
        {
            return Result.Failure<SoldUnits>(
                SoldUnitsErrors.SoldUnitsCannotBeNegative);
        }

        return new SoldUnits(soldUnits);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}