namespace Domain.LanguageAggregate.Errors;

public static class LanguageErrors
{
    public static Error IsNotExist =>
        new("Language.IsNotExist", "Language is not exist.");
}
