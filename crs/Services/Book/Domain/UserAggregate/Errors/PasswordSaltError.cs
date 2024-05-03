namespace Domain.UserAggregate.Errors;

public static class PasswordSaltError
{
    public static Error PasswordSaltIsInvalid(string passwordSalt) =>
        new("PasswordSalt.PasswordSaltIsInvalid", $"Password salt '{passwordSalt}' is invalid.");

    public static Error CannotBeEmpty =>
        new("PasswordSalt.CannotBeEmpty", "Password salt cannot be empty.");

    public static Error TooShort =>
        new("PasswordSalt.TooShort", "Password salt is too short.");
}
