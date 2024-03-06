using Domain.BookAggregate.Ids;

namespace Domain.BookAggregate.Repositories;

public interface IBookRepository : IBaseRepository<Book, BookId>
{
}
