using Domain.OrderAggregate.Ids;
using Domain.SharedKernel.Repositories;

namespace Domain.OrderAggregate.Repositories;

public interface IOrderRepository : IBaseRepository<Order, OrderId>
{
    Task<IReadOnlyList<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Order> GetOrderWithItemsAsync(OrderId orderId, CancellationToken cancellationToken = default);
}
