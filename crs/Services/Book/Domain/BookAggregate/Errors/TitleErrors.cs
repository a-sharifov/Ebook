namespace Domain.BookAggregate.Errors;

public static class TitleErrors
{
    public static Error CannotBeEmpty =>
       new("Title.CannotBeEmpty", "title cannot be empty");

    public static Error CannotBeLongerThan(int maxLength) =>
        new("Title.CannotBeLongerThan", $"title cannot be longer than {maxLength}");
}