using Domain.OrderAggregate.Errors;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class OrderRepository : BaseRepository<Order, OrderId>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> GetOrderWithItemsAsync(OrderId orderId, CancellationToken cancellationToken = default)
    {
        if (!await IsExistAsync(orderId, cancellationToken))
        {
            throw new InvalidOperationException($"Order with ID {orderId} not found");
        }

        return await _dbContext.Orders
            .Include(o => o.Items)
            .FirstAsync(o => o.Id.Equals(orderId), cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate.Value)
            .ToListAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        _dbContext.Update(order);
    }
}
