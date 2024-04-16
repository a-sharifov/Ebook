namespace Domain.BookAggregate.Regexes;

public partial class ISBNRegex
{
    public const string ISBNPattern = @"^(97(8|9))?\d{9}(\d|X)$";

    [GeneratedRegex(ISBNPattern)]
    public static partial Regex Regex();
}