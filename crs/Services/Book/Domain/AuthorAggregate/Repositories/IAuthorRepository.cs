using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.ValueObjects;

namespace Domain.AuthorAggregate.Repositories;

public interface IAuthorRepository : IBaseRepository<Author, AuthorId>
{
    Task<bool> IsExistsAsync(Pseudonym pseudonym, CancellationToken cancellationToken = default);
    Task<Author> GetAsync(Pseudonym pseudonym, CancellationToken cancellationToken = default);
}
