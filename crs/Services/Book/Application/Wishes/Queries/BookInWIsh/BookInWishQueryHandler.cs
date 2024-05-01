using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Repositories;

namespace Application.Wishes.Queries.BookInWIsh;

internal sealed class BookInWishQueryHandler(
    IWishRepository repository) 
    : IQueryHandler<BookInWishQuery, bool>
{
    private readonly IWishRepository _repository = repository;

    public async Task<Result<bool>> Handle(BookInWishQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var bookId = new BookId(request.BookId);

        var isExist = await _repository.BookIsExistInWishAsync(userId, bookId, cancellationToken);

        return isExist;
    }
}

