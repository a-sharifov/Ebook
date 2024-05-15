using Domain.UserAggregate.Errors;

namespace Domain.UserAggregate.ValueObjects;

public sealed class EmailConfirmationToken : ValueObject
{
    public string Value { get; private set; }

    private EmailConfirmationToken(string value) =>
        Value = value;

    public static EmailConfirmationToken Create()
    {
        var emailConfirmationToken = new EmailConfirmationToken(Guid.NewGuid().ToString());
        return emailConfirmationToken;
    }

    public static Result<EmailConfirmationToken> Create(string emailConfirmationToken)
    {
        if (emailConfirmationToken.IsNullOrWhiteSpace())
        {
            return Result.Failure<EmailConfirmationToken>(
                EmailConfirmationTokenErrors.InvalidEmailConfirmationToken());
        }

        return Result.Success(new EmailConfirmationToken(emailConfirmationToken));
    }

    public static implicit operator EmailConfirmationToken(string token) =>
        Create(token).Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
