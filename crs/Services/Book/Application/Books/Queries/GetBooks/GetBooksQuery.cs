﻿using Application.Common.DTOs.Books;
using Contracts.Paginations;

namespace Application.Books.Queries.GetBooks;

public sealed record GetBooksQuery(
    int PageNumber,
    int PageSize,
    decimal MinPrice = 0,
    decimal MaxPrice = 0,
    string? Title = default,
    Guid LanguageId = default,
    Guid AuthorId = default,
    Guid GenreId = default
    ) : IQuery<PagedList<BookDto>>;
