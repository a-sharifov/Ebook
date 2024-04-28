namespace Domain.BookAggregate.Errors;

public static class BookErrors
{
    public static Error BookIsNotExist =>
          new("Book.BookIsNotExists", "Book is not exists");
}
