using Catalog.Persistence.Caching.Abstractions;
using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class LanguageRepository(
    BookDbContext dbContext,
    ICachedEntityService<Language, LanguageId> cached)
    : BaseRepository<Language, LanguageId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), ILanguageRepository
{
}