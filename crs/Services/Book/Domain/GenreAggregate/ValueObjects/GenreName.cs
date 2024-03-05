using Domain.AuthorAggregate.Errors;
using Domain.AuthorAggregate.ValueObjects;
using Domain.GenreAggregate.Errors;

namespace Domain.GenreAggregate.ValueObjects;

public class GenreName : ValueObject
{
    public string Value { get; private set; }

    public const int MaxLength = 50;

    private GenreName(string value) =>
        Value = value;

    public static Result<GenreName> Create(string pseudonym)
    {
        if (pseudonym.IsNullOrWhiteSpace())
        {
            return Result.Failure<GenreName>(
                GenreNameErrors.CannotBeEmpty);
        }

        if (pseudonym.Length > MaxLength)
        {
            return Result.Failure<GenreName>(
                GenreNameErrors.CannotBeLongerThan(MaxLength));
        }

        return new GenreName(pseudonym);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}