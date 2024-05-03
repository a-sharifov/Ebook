namespace Domain.UserAggregate.Errors;

public class RefreshTokenErrors
{
    public static Error CannotBeEmpty =>
        new("RefreshToken.CannotBeEmpty", "Refresh token should not be empty");

    public static Error CannotBeExpired =>
        new("RefreshToken.CannotBeExpired", "Refresh token should not be expired");
}
