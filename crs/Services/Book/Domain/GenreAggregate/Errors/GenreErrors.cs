namespace Domain.GenreAggregate.Errors;

public static class GenreErrors
{
    public static Error IsNameExist =>
       new("Genre.IsNameExist", "Genre name exist.");

    public static Error IsNotExist =>
     new("Genre.IsNotExist", "Genre is not exist.");
}