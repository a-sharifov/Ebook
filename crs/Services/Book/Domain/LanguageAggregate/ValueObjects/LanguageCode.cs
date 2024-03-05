using Domain.LanguageAggregate.Errors;

namespace Domain.LanguageAggregate.ValueObjects;

public class LanguageCode : ValueObject
{
    public string Value { get; private set; }
    public const int LanguageCodeMaxLength = 35;

    private LanguageCode(string value) => Value = value;

    public static Result<LanguageCode> Create(string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return Result.Failure<LanguageCode>(
                LanguageCodeErrors.CannotBeEmpty);
        }

        if (value.Length > LanguageCodeMaxLength)
        {
            return Result.Failure<LanguageCode>(
                LanguageCodeErrors.CannotBeLongerThan(LanguageCodeMaxLength));
        }

        return new LanguageCode(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}