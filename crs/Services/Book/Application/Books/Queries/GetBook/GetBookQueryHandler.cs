using Application.Common.DTOs.Books;
using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;

namespace Application.Books.Queries.GetBook;

internal sealed class GetBookQueryHandler(IBookRepository repository) : IQueryHandler<GetBookQuery, BookDto>
{
    private readonly IBookRepository _repository = repository;

    public async Task<Result<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<BookDto>(
                BookErrors.BookIsNotExist);
        }

        var book = await _repository.GetAsync(id, cancellationToken);
        var bookDto = book.Adapt<BookDto>();

        return bookDto;
    }
}

