using Domain.AuthorAggregate.Errors;

namespace Domain.AuthorAggregate.ValueObjects;

public class AuthorDescription : ValueObject
{
    public string Value { get; private set; }
    public const int AuthorDescriptionMaxLength = 1000;

    private AuthorDescription(string value) => Value = value;

    public static Result<AuthorDescription> Create(string authorDescription)
    {
        if (authorDescription.Length > AuthorDescriptionMaxLength)
        {
            return Result.Failure<AuthorDescription>(
                AuthorDescriptionErrors.CannotBeLongerThan(AuthorDescriptionMaxLength));
        }

        return new AuthorDescription(authorDescription);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}