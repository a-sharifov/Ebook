namespace Domain.LanguageAggregate.Errors;

public static class LanguageCodeErrors
{
    public static Error CannotBeEmpty => 
        new("Language.CannotBeEmpty", "code cannot be empty");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("Language.CannotBeLongerThan", $"code cannot be longer than {maxLength}");
}