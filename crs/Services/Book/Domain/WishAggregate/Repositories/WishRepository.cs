using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Ids;

namespace Domain.WishAggregate.Repositories;

public interface IWishRepository : IBaseRepository<Wish, WishId>
{
    Task<bool> BookIsExistInWishAsync(UserId userId, BookId bookId, CancellationToken cancellationToken);
    Task<Wish> GetAsync(UserId id, CancellationToken cancellationToken = default);
}
