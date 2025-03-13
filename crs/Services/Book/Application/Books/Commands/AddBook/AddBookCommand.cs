namespace Application.Books.Commands.AddBook;

public sealed record AddBookCommand(
    string Title,
    string Description,
    int PageCount,
    decimal Price,
    Guid LanguageId,
    int Quantity,
    Guid AuthorId,
    Guid GenreId,
    string Poster,
    Stream PosterStream
    ) : ICommand;