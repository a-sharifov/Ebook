using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Ids;

namespace Domain.WishAggregate.Repositories;

public interface IWishRepository : IBaseRepository<Wish, WishId>
{
    Task<Wish> GetAsync(UserId id, CancellationToken cancellationToken = default);
}
