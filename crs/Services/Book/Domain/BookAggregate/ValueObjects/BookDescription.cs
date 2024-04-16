using Domain.BookAggregate.Errors;

namespace Domain.BookAggregate.ValueObjects;

public class BookDescription : ValueObject
{
    public string Value { get; private set; }
    public const int BookDescriptionMaxLength = 1000;

    private BookDescription(string value) => Value = value;

    public static Result<BookDescription> Create(string value)
    {
        if (value.Length > BookDescriptionMaxLength)
        {
            return Result.Failure<BookDescription>(
                BookDescriptionErrors.CannotBeLongerThan(BookDescriptionMaxLength));
        }

        return new BookDescription(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
