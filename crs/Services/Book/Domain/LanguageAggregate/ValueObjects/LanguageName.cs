using Domain.LanguageAggregate.Errors;

namespace Domain.LanguageAggregate.ValueObjects;

public class LanguageName : ValueObject
{
    public string Value { get; private set; }
    public const int LanguageNameMaxLength = 100;

    private LanguageName(string value) => Value = value;

    public static Result<LanguageName> Create(string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return Result.Failure<LanguageName>(
                LanguageNameErrors.CannotBeEmpty);
        }

        if (value.Length > LanguageNameMaxLength)
        {
            return Result.Failure<LanguageName>(
                LanguageNameErrors.CannotBeLongerThan(LanguageNameMaxLength));
        }

        return new LanguageName(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}








