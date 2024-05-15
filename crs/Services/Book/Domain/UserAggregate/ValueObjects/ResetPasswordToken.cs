using Domain.UserAggregate.Errors;

namespace Domain.UserAggregate.ValueObjects;

public sealed class ResetPasswordToken : ValueObject
{
    public string Value { get; }

    private ResetPasswordToken(string value)
    {
        Value = value;
    }

    public static ResetPasswordToken Create()
    {
        var resetPasswordToken = new ResetPasswordToken(Guid.NewGuid().ToString());
        return resetPasswordToken;
    }

    public static Result<ResetPasswordToken> Create(string value)
    {
        if (value.IsNullOrEmpty())
        {
            return Result.Failure<ResetPasswordToken>(
                ResetPasswordTokenErrors.CannotBeEmpty);
        }

        var resetPasswordToken = new ResetPasswordToken(value);
        return resetPasswordToken;
    }

    public static implicit operator string(ResetPasswordToken token) => token.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
