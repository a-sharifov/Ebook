namespace Application.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    Guid Id,
    string Title,
    string Description,
    int PageCount,
    decimal Price,
    Guid LanguageId,
    int Quantity,
    Guid AuthorId,
    Guid GenreId,
    string? Poster,
    Stream? PosterStream
    ) : ICommand;
