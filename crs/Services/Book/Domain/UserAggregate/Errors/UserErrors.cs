namespace Domain.UserAggregate.Errors;

public static class UserErrors
{
    public static Error EmailIsNotUnique(string email) =>
        new("User.EmailIsNotUnique", $"Email {email} is not unique.");

    public static Error EmailIsNotConfirmed =>
        new("User.EmailIsNotConfirmed", $"Email is not confirmed.");

    public static Error PasswordIsNotCorrect =>
        new("User.PasswordIsNotCorrect", $"Password is not correct.");

    public static Error OldPasswordIsNotCorrect =>
      new("User.OldPasswordIsNotCorrect", $"Old password is not correct.");

    public static Error UserDoesNotExist =>
        new("User.UserDoesNotExist", $"User does not exist.");

    public static Error EmailIsNotExists(string email) => 
        new("User.EmailIsNotExists", $"Email {email} is not exists.");

    public static Error RefreshTokenIsNotCorrect =>
        new("User.RefreshTokenIsNotExists", $"Refresh token is not exists.");

    public static Error RefreshTokenIsExpired => 
        new("User.RefreshTokenIsNotValid", $"Refresh token is expired.");

    public static Error EmailConfirmationtokenIsnotCorrect =>
        new("User.EmailConfirmationtokenIsnotCorrect", $"Email confirmation token is not correct.");

    public static Error EmailIsAlreadyConfirmed =>
        new("User.EmailIsAlreadyConfirmed", $"Email is already confirmed.");

    public static Error BookIsAlreadyInWishList =>
        new("User.BookIsAlreadyInWishList", $"Book is a;ready in wish list.");

    public static Error BookIsNotInWishList =>
        new("User.BookIsNotInWishList", $"Book is not in wish list.");

    public static Error UserIsConfirmEmail =>
        new("User.UserIsConfirmed", $"User is confirm email.");

    public static Error IsCurrentEmail =>
      new("User.IsCurrentEmail", $"Is current email.");

    public static Error ResetPasswordTokenIsNotSet =>
        new("User.ResetPasswordTokenIsNotSet", $"Reset password token is not set.");

    public static Error ResetPasswordTokenIsExpired =>
        new("User.ResetPasswordTokenIsExpired", $"Reset password token is expired.");

    public static Error ResetPasswordTokenIsNotCorrect =>
        new("User.ResetPasswordTokenIsNotCorrect", $"Reset password token is not correct.");
}
