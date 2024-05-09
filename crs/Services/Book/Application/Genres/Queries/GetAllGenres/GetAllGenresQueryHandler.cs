using Application.Common.DTOs.Genres;
using Domain.GenreAggregate.Repositories;

namespace Application.Genres.Queries.GetAllGenres;

internal sealed class GetAllGenresQueryHandler(IGenreRepository repository) : IQueryHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<Result<IEnumerable<GenreDto>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _repository.GetAllAsync(cancellationToken: cancellationToken);

        var genresDto = genres.Adapt<IEnumerable<GenreDto>>();

        return Result.Success(genresDto);
    }
}