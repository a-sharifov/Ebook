using Domain.UserAggregate.Errors;

namespace Domain.UserAggregate.ValueObjects;

public sealed class RefreshToken : ValueObject
{
    public string Token { get; private set; }
    public DateTime ExpiredTime { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiredTime;

    private RefreshToken(string token, DateTime expiredTime) =>
        (Token, ExpiredTime) = (token, expiredTime);

    public static Result<RefreshToken> Create(string refreshToken, DateTime expiredTime)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Result.Failure<RefreshToken>(
                RefreshTokenErrors.CannotBeEmpty);
        }

        if (expiredTime < DateTime.UtcNow)
        {
            return Result.Failure<RefreshToken>(
                RefreshTokenErrors.CannotBeExpired);
        }

        return new RefreshToken(refreshToken, expiredTime);
    }

    public static RefreshToken Empty =>
        new(string.Empty, DateTime.UtcNow);

    public static implicit operator string(RefreshToken token) =>
        token.Token;


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Token;
        yield return ExpiredTime;
    }
}
