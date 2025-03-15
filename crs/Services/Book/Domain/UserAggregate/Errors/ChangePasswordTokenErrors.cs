namespace Domain.UserAggregate.Errors;

public static class ChangePasswordTokenErrors
{
    public static Error InvalidForgotPasswordToken() =>
        new("invalid_change_password_token", "Invalid change password token.");
}