using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;

namespace Domain.CartAggregate.Repositories;

public interface ICartItemRepository : IBaseRepository<CartItem, CartItemId>
{

}
