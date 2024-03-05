namespace Domain.BookAggregate.Errors;

public static class BookDescriptionErrors
{
    public static Error CannotBeLongerThan(int maxLength) =>
       new("Book.CannotBeLongerThan", $"description cannot be longer than {maxLength}");
}