using Domain.BookAggregate.Errors;

namespace Domain.BookAggregate.ValueObjects;

public class Title : ValueObject
{
    public string Value { get; private set; }

    public const int MaxLength = 100;

    private Title(string value) => Value = value;

    public static Result<Title> Create(string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return Result.Failure<Title>(
                TitleErrors.CannotBeEmpty);
        }

        if (value.Length > MaxLength)
        {
            return Result.Failure<Title>(
                TitleErrors.CannotBeLongerThan(MaxLength));
        }

        return new Title(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
