using Catalog.Persistence.Caching.Abstractions;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class WishRepository(
    BookDbContext dbContext,
    ICachedEntityService<Wish, WishId> cached)
    : BaseRepository<Wish, WishId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20),
        enableBaseCaching: false), IWishRepository
{
    public async Task<Wish> GetAsync(UserId id, CancellationToken cancellationToken = default)
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

        var wish = await query.FirstAsync(
          x => x.UserId == id, cancellationToken: cancellationToken);

        return wish;
    }

}
