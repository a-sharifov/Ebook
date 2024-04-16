using Catalog.Persistence.Caching.Abstractions;
using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
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
}
