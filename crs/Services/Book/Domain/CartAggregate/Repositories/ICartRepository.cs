using Domain.BookAggregate.Ids;
using Domain.CartAggregate.Ids;
using Domain.UserAggregate.Ids;

namespace Domain.CartAggregate.Repositories;

public interface ICartRepository : IBaseRepository<Cart, CartId>
{
    Task<bool> BookIsExistInCartAsync(UserId userId, BookId bookId, CancellationToken cancellationToken = default);
    Task<Cart> GetAsync(UserId id, CancellationToken cancellationToken = default);
    Task<int> GetTotalQuantityBookAsync(UserId id, CancellationToken cancellationToken = default);
}