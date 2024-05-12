using Domain.UserAggregate.Errors;

namespace Domain.UserAggregate.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public const int MinLength = 8;
    public const int MaxLength = 100;

    public string Value { get; private set; }

    private PasswordHash(string value) =>
        Value = value;

    public static Result<PasswordHash> Create(string passwordHash)
    {
        if (passwordHash.IsNullOrWhiteSpace())
        {
            return Result.Failure<PasswordHash>(
                PasswordHashErrors.CannotBeEmpty);
        }

        if (passwordHash.Length < MinLength)
        {
            return Result.Failure<PasswordHash>(
                PasswordHashErrors.CannotBeShorterThan(MinLength));
        }

        if (passwordHash.Length > MaxLength)
        {
            return Result.Failure<PasswordHash>(
                PasswordHashErrors.CannotBeLongerThan(MaxLength));
        }

        return new PasswordHash(passwordHash);
    }

    public static implicit operator string(PasswordHash hash) =>
        hash.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
 