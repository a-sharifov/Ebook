using Catalog.Persistence.Caching.Abstractions;
using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
using Domain.AuthorAggregate.ValueObjects;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class AuthorRepository(
    BookDbContext dbContext,
    ICachedEntityService<Author, AuthorId> cached)
    : BaseRepository<Author, AuthorId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IAuthorRepository
{
    public async Task<Author> GetAsync(Pseudonym pseudonym, CancellationToken cancellationToken = default)
    {
        var author = await GetEntityDbSet()
            .Where(x => x.Pseudonym == pseudonym)
            .FirstAsync(cancellationToken: cancellationToken);

        await _cached.SetAsync(author, cancellationToken: cancellationToken);

        return author;
    }

    public async Task<bool> IsExistsAsync(Pseudonym pseudonym, CancellationToken cancellationToken = default)
    {
        var isExists = await GetEntityDbSet()
           .Where(x => x.Pseudonym == pseudonym)
           .AnyAsync(cancellationToken: cancellationToken);

        return isExists;
    }
}
