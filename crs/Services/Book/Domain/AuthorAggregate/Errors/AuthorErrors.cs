namespace Domain.AuthorAggregate.Errors;

public static class AuthorErrors
{
    public static Error NotFound =>
       new("Author.NotFound", "The Author was not found.");

    public static Error IsExist =>
     new("Author.IsExist", "The Author is exist.");

    public static Error IsNotExist =>
    new("Author.IsNotExist", "Author is not exist.");
}
