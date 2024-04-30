using Application.Common.DTOs.Books;

namespace Application.Books.Queries.GetBook;

public sealed record GetBookQuery(Guid Id) : IQuery<BookDto>;

