using Domain.AuthorAggregate.Errors;

namespace Domain.AuthorAggregate.ValueObjects;

public class Pseudonym : ValueObject
{
    public const int MaxLength = 100;

    public string Value { get; private set; }

    private Pseudonym(string value) =>
        Value = value;

    public static Result<Pseudonym> Create(string pseudonym)
    {
        if (pseudonym.IsNullOrWhiteSpace())
        {
            return Result.Failure<Pseudonym>(
                PseudonymErrors.CannotBeEmpty);
        }

        if (pseudonym.Length > MaxLength)
        {
            return Result.Failure<Pseudonym>(
                PseudonymErrors.CannotBeLongerThan(MaxLength));
        }

        return new Pseudonym(pseudonym);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}