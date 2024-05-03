namespace Presentation.V1.Books.Models;

public sealed record GetBooksRequest(
    [Required] int PageSize,
    int PageNumber = 1,
    decimal MinPrice = 0,
    decimal MaxPrice = 0,
    string? Title = default,
    Guid LanguageId = default,
    Guid AuthorId = default,
    Guid GenreId = default
    );
