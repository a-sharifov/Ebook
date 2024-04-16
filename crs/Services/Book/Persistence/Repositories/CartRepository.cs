using Catalog.Persistence.Caching.Abstractions;
using Domain.CartAggregate;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class CartRepository(
    BookDbContext dbContext,
    ICachedEntityService<Cart, CartId> cached)
    : BaseRepository<Cart, CartId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), ICartRepository
{
}
