namespace Domain.LanguageAggregate.Errors;

public static class LanguageCodeErrors
{
    public static Error CannotBeEmpty => 
        new("LanguageCode.CannotBeEmpty", "code cannot be empty");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("LanguageCode.CannotBeLongerThan", $"code cannot be longer than {maxLength}");
}
