using Domain.GenreAggregate.Ids;

namespace Domain.GenreAggregate.Repositories;

public interface IGenreRepository : IBaseRepository<Genre, GenreId>
{
}
