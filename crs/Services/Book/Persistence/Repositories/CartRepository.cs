using Catalog.Persistence.Caching.Abstractions;
using Domain.CartAggregate;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.UserAggregate.Ids;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class CartRepository(
    BookDbContext dbContext,
    ICachedEntityService<Cart, CartId> cached)
    : BaseRepository<Cart, CartId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20),
        enableBaseCaching: false), ICartRepository
{
    public async Task<Cart> GetAsync(UserId id, CancellationToken cancellationToken = default)
    {
        var query = GetEntityDbSet()
          .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .ThenInclude(x => x.Genre)
          .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .ThenInclude(x => x.Poster)
          .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .ThenInclude(x => x.Language)
          .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .ThenInclude(x => x.Author);

        var cart = await query.FirstAsync(
            x => x.UserId == id, cancellationToken: cancellationToken);

        return cart;
    }

    public async Task<IEnumerable<Cart>> GetExpiredAsync(int take, CancellationToken cancellationToken = default)
    {
        var carts = await GetEntityDbSet()
            .Where(x => x.ExpirationTime >= DateTime.UtcNow)
            .Take(take)
            .ToListAsync(cancellationToken: cancellationToken);

        return carts;
    }
}
