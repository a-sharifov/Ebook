using Domain.AuthorAggregate.Ids;

namespace Domain.AuthorAggregate.Repositories;

public interface IAuthorRepository : IBaseRepository<Author, AuthorId>
{
}
