using Domain.CartAggregate.Ids;
using Domain.UserAggregate.Ids;

namespace Domain.CartAggregate.Repositories;

public interface ICartRepository : IBaseRepository<Cart, CartId>
{
    Task<Cart> GetAsync(UserId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cart>> GetExpiredAsync(int take, CancellationToken cancellationToken = default);
}
