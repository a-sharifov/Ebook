using Catalog.Persistence.Caching.Abstractions;
using Domain.BookAggregate.Ids;
using Domain.CartAggregate;
using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.UserAggregate.Ids;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class CartRepository(
    BookDbContext dbContext,
    ICachedEntityService<Cart, CartId> cached)
    : BaseRepository<Cart, CartId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20),
        enableBaseCaching: false), ICartRepository
{
    public async Task<bool> BookIsExistInCartAsync(UserId userId, BookId bookId, CancellationToken cancellationToken = default)
    {
        var isExist = await GetEntityDbSet()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .SelectMany(x => x.Items)
            .AnyAsync(x => x.Book.Id == bookId, cancellationToken: cancellationToken);

            return isExist;
    }

    public async Task<Cart> GetAsync(UserId userId, CancellationToken cancellationToken = default)
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
            x => x.UserId == userId, cancellationToken: cancellationToken);

        return cart;
    }

    public async Task<int> GetTotalQuantityBookAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var quantity = await GetEntityDbSet()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .SelectMany(x => x.Items)
            .Select(x => (int)x.Quantity)
            .SumAsync(cancellationToken: cancellationToken);

        return quantity;
    }

    public async Task<IReadOnlyCollection<CartItem>> GetItemAsync(UserId userId, CancellationToken cancellationToken = default)
    {  
        var item = await GetEntityDbSet()
            .Where(x => x.UserId == userId)
            .Select(x => x.Items)
            .FirstAsync(cancellationToken: cancellationToken);

        return item;
    }
}
