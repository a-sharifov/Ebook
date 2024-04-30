﻿using Application.Common.DTOs.Books;
using Contracts.Paginations;
using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.Repositories.Requests;

namespace Application.Books.Queries.GetBooks;

internal sealed class GetBooksQueryHandler(
    IBookRepository repository) 
    : IQueryHandler<GetBooksQuery, PagedList<BookDto>>
{
    private readonly IBookRepository _repository = repository;

    public async Task<Result<PagedList<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var filterRequest = new BookFilterRequest(
            request.MinPrice,
            request.MaxPrice,
            request.Title,
            request.LanguageId,
            request.AuthorId,
            request.GenreId);

        var bookPagedList = await _repository.GetByFilterAsync(
            filterRequest, request.PageNumber, request.PageSize, cancellationToken);

        var bookDtoPagedList = bookPagedList.Adapt<PagedList<BookDto>>();

        return bookDtoPagedList;
    }
} 