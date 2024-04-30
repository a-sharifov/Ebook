using Catalog.Persistence.Caching.Abstractions;
using Contracts.Paginations;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.Repositories.Requests;
using Persistence.DbContexts;
using Persistence.Filters;

namespace Persistence.Repositories;

public class BookRepository(
    BookDbContext dbContext,
    ICachedEntityService<Book, BookId> cached)
    : BaseRepository<Book, BookId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(2)), IBookRepository
{
    public async Task<PagedList<Book>> GetByFilterAsync(
        BookFilterRequest request, 
        int pageNumber, 
        int pageSize, CancellationToken cancellationToken = default) =>
       await GetPagedAsync(
            pageNumber,
            pageSize,
            wheres: [..  BookFiltersBuilder.Build(request)],
            includes: [
                x => x.Language,
                x => x.Author,
                x => x.Genre,
                x => x.Poster
            ],
            cancellationToken
            );
}
