using Domain.BookAggregate.Errors;

namespace Domain.BookAggregate.ValueObjects;

public class PageCount : ValueObject
{
    public int Value { get; private set; }

    private PageCount(int value) => Value = value;

    public static Result<PageCount> Create(int value)
    {
        if (value == 0)
        {
            return Result.Failure<PageCount>(
                PageCountErrors.CannotBeNullPage);
        }

        return new PageCount(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}