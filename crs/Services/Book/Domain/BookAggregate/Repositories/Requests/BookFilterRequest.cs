namespace Domain.BookAggregate.Repositories.Requests;

public sealed record BookFilterRequest(
    decimal MinPrice = 0,
    decimal MaxPrice = 0,
    string? Title = default,
    Guid LanguageId = default,
    Guid AuthorId = default,
    Guid GenreId = default
    );
