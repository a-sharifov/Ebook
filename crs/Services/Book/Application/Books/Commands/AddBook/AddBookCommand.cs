namespace Application.Books.Commands.AddBook;

public sealed record AddBookCommand(
    string Title,
    string Description,
    int PageCount,
    decimal Price,
    Guid LanguageId,
    int Quantity,
    string AuthorPseudonym,
    Guid GenreId,
    string Poster,
    Stream PosterStream
    ) : ICommand;