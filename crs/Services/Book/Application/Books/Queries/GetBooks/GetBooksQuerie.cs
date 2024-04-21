namespace Application.Books.Queries.GetBooks;

public sealed record GetBooksQuerie(
    int Skip = default,
    int Take = 8,
    string? Genre = default,
    string? Title = default,
    string? Author = default,
    decimal LowPrice = default,
    decimal MaxPrice = default);
 
