using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.ValueObjects;

namespace Domain.GenreAggregate.Repositories;

public interface IGenreRepository : IBaseRepository<Genre, GenreId>
{
    Task<bool> IsNameExistAsync(GenreName name, CancellationToken cancellationToken = default);
}
