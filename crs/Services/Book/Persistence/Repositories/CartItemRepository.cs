using Catalog.Persistence.Caching.Abstractions;
using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class CartItemRepository(BookDbContext dbContext,
    ICachedEntityService<CartItem, CartItemId> cached)
    : BaseRepository<CartItem, CartItemId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20),
        enableBaseCaching: false), ICartItemRepository
{
    public new async Task<CartItem> GetAsync(CartItemId itemId, CancellationToken cancellationToken = default)
    {
        var cartItem = await _dbContext.Carts.SelectMany(x => x.Items)
            .AsNoTracking()
            .Where(x => x.Id == itemId)
            .Include(x => x.Book)
            .FirstAsync(cancellationToken: cancellationToken);

        return cartItem;
    }
}
