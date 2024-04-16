namespace Domain.BookAggregate.Errors;

public static class PageCountErrors
{
    public static Error CannotBeNullPage =>
        new("PageCount.CannotBeNullPage", "Page count cannot be null");
}