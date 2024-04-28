namespace Domain.GenreAggregate.Errors;

public static class GenreNameErrors
{
    public static Error CannotBeEmpty =>
        new("GenreName.CannotBeEmpty", "Genre name cannot be empty.");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("GenreName.CannotBeLongerThan", $"Genre name name cannot be longer than {maxLength} characters.");
}
