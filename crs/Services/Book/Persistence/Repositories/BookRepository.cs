using Catalog.Persistence.Caching.Abstractions;
using Contracts.Paginations;
using Domain.BookAggregate;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.Repositories.Requests;
using Infrasctrurcture.Core.Extensions;
using Persistence.DbContexts;
using Persistence.Filters;

namespace Persistence.Repositories;

public sealed class BookRepository(
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
            wheres: [.. BookFiltersBuilder.Build(request)],
            includes: [
                x => x.Language,
                x => x.Author,
                x => x.Genre,
                x => x.Poster
            ],
            cancellationToken
            );

    public new async Task<Book> GetAsync(BookId id, CancellationToken cancellationToken = default)
    {
        var book = await _cached.GetAsync(id, cancellationToken) ?? await GetEntityDbSet().Includes(
            x => x.Language,
            x => x.Author,
            x => x.Genre,
            x => x.Poster
            ).FirstAsync(x => x.Id == id, cancellationToken: cancellationToken);

        await _cached.SetAsync(book, _cachingBaseExpirationTime, cancellationToken);
        return book;
    }
}
