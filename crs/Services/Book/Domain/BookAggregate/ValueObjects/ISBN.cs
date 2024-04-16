using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Regexes;

namespace Domain.BookAggregate.ValueObjects;

public class ISBN : ValueObject
{
    public string Value { get; private set; }

    private ISBN(string value) => Value = value;

    public static Result<ISBN> Create(string isbn)
    {
        if (isbn.IsNullOrWhiteSpace())
        {
            return Result.Failure<ISBN>(
                ISBNErrors.CannotBeEmpty);
        }

        isbn = isbn.Trim();

        if (!IsISBN(isbn))
        {
            return Result.Failure<ISBN>(
                ISBNErrors.IsInvalid);
        }

        return new ISBN(isbn);
    }

    public static bool IsISBN(string isbn) =>
        ISBNRegex.Regex().IsMatch(isbn);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}