namespace Domain.GenreAggregate.Errors;

public static class GenreErrors
{
    public static Error IsNameExist =>
       new("Genre.IsNameExist", "Is name exist.");

    public static Error IsNotExist =>
     new("Genre.IsNotExist", "Is not exist.");
}