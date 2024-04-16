using Catalog.Persistence.Caching.Abstractions;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.GenreAggregate;
using Persistence.DbContexts;
using System.Threading;

namespace Persistence.Repositories;

public class BookRepository(
    BookDbContext dbContext,
    ICachedEntityService<Book, BookId> cached)
    : BaseRepository<Book, BookId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(2)), IBookRepository
{



}
