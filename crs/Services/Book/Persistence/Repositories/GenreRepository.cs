using Catalog.Persistence.Caching.Abstractions;
using Domain.GenreAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Domain.GenreAggregate.ValueObjects;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public sealed class GenreRepository(
    BookDbContext dbContext,
    ICachedEntityService<Genre, GenreId> cached)
    : BaseRepository<Genre, GenreId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IGenreRepository
{

    public async Task<bool> IsNameExistAsync(GenreName name, CancellationToken cancellationToken = default)
    {
        var isExist =  await GetEntityDbSet().AnyAsync(x => x.Name == name, cancellationToken);
        return isExist;
    }
}
