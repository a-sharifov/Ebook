using Domain.BookAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Queries.BookInCart;

public sealed class BookInCartQueryHandler(ICartRepository repository) : IQueryHandler<BookInCartQuery, bool>
{
    private readonly ICartRepository _repository = repository;

    public async Task<Result<bool>> Handle(BookInCartQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var bookId = new BookId(request.BookId);

        var isExist = await _repository.BookIsExistInCartAsync(userId, bookId, cancellationToken);

        return isExist;
    }
}
