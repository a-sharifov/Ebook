using Application.Common.DTOs.Books;

namespace Application.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(Guid Id) : IQuery<BookDto>;

