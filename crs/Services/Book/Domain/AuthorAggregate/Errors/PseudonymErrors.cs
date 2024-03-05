namespace Domain.AuthorAggregate.Errors;

public static class PseudonymErrors
{
    public static Error CannotBeEmpty =>
       new("Pseudonym.CannotBeEmpty", "Pseudonym cannot be empty.");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("Pseudonym.CannotBeLongerThan", $"Pseudonym name cannot be longer than {maxLength} characters");
}