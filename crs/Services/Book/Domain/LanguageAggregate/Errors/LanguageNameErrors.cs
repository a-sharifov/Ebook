namespace Domain.LanguageAggregate.Errors;

public static class LanguageNameErrors
{
    public static Error CannotBeEmpty => 
        new("Language.CannotBeEmpty", "name cannot be empty");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("Language.CannotBeLongerThan", $"name cannot be longer than {maxLength}");
}