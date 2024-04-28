using Contracts.Paginations;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories.Requests;

namespace Domain.BookAggregate.Repositories;

public interface IBookRepository : IBaseRepository<Book, BookId>
{
    Task<PagedList<Book>> GetByFilterAsync(BookFilterRequest request, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
