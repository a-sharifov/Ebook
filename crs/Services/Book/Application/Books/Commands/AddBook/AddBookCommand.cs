namespace Application.Books.Commands.AddBook;

public sealed record AddBookCommand(
    string Title,
    string Description,
    int PageCount,
    decimal Price,
    string ISBN,
    Guid LanguageId,
    int Quantity,
    Guid AuthorId,
    string Poster,
    Stream PosterStream    
    ) : ICommand;
