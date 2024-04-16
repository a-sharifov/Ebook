using Catalog.Persistence.Caching.Abstractions;
using Domain.GenreAggregate;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class GenreRepository(
    BookDbContext dbContext,
    ICachedEntityService<Genre, GenreId> cached)
    : BaseRepository<Genre, GenreId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IGenreRepository
{
}
