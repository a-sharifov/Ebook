using Catalog.Persistence.Caching.Abstractions;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class BookRepository(
    BookDbContext dbContext,
    ICachedEntityService<Book, BookId> cached)
    : BaseRepository<Book, BookId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IBookRepository
{
}
