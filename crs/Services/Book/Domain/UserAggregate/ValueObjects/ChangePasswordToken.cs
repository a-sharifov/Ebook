using Domain.UserAggregate.Errors;

namespace Domain.UserAggregate.ValueObjects;

public sealed class ChangePasswordToken : ValueObject
{
    public string Value { get; private set; }

    private ChangePasswordToken(string value) =>
        Value = value;

    public static ChangePasswordToken Create()
    {
        var changeConfirmationToken = new ChangePasswordToken(Guid.NewGuid().ToString());
        return changeConfirmationToken;
    }

    public static Result<ChangePasswordToken> Create(string changeConfirmationToken)
    {
        if (changeConfirmationToken.IsNullOrWhiteSpace())
        {
            return Result.Failure<ChangePasswordToken>(
                ChangePasswordTokenErrors.InvalidForgotPasswordToken());
        }

        return Result.Success(new ChangePasswordToken(changeConfirmationToken));
    }

    public static implicit operator ChangePasswordToken(string token) =>
        Create(token).Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
