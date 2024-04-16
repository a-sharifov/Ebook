using Domain.CartAggregate.Ids;

namespace Domain.CartAggregate.Repositories;

public interface ICartRepository : IBaseRepository<Cart, CartId>
{
}
