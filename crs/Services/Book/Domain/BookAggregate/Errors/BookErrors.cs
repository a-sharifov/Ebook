namespace Domain.BookAggregate.Errors;

public static class BookErrors
{
    public static Error BookIsNotExists =>
          new("Book.BookIsNotExists", "Book is not exists");
}
