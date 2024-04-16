namespace Domain.BookAggregate.Errors;

public static class ISBNErrors
{
    public static Error CannotBeEmpty =>
        new("ISBN.CannotBeEmpty", "ISBN cannot be empty");

    public static Error IsInvalid =>
        new("ISBN.IsInvalid", "ISBN is invalid");
}