using Catalog.Persistence.Caching.Abstractions;
using Domain.BookAggregate.Ids;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class WishRepository(
    BookDbContext dbContext,
    ICachedEntityService<Wish, WishId> cached)
    : BaseRepository<Wish, WishId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20),
        enableBaseCaching: false), IWishRepository
{
    public async Task<bool> BookIsExistInWishAsync(UserId userId, BookId bookId, CancellationToken cancellationToken)
    {
        var isExist = await GetEntityDbSet()
           .AsNoTracking()
           .Where(x => x.UserId == userId)
           .SelectMany(x => x.Items)
           .AnyAsync(x => x.Book.Id == bookId, cancellationToken: cancellationToken);

        return isExist;
    }

    public async Task<Wish> GetAsync(UserId userId, CancellationToken cancellationToken = default)
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
          x => x.UserId == userId, cancellationToken: cancellationToken);

        return wish;
    }

}
