namespace Domain.AuthorAggregate.Errors;

public static class AuthorDescriptionErrors
{
    public static Error CannotBeLongerThan(int maxLength) =>
     new("AuthorDescription.CannotBeLongerThan", $"description cannot be longer than {maxLength}");
}