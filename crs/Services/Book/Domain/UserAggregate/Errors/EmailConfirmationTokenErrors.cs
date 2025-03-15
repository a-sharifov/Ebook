namespace Domain.UserAggregate.Errors;

public static class EmailConfirmationTokenErrors
{
    public static Error InvalidEmailConfirmationToken() =>
        new("invalid_email_confirmation_token", "Invalid email confirmation token.");
}
