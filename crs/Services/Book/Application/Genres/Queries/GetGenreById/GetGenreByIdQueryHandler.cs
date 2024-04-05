using Application.Common.DTOs.Genres;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;

namespace Application.Genres.Queries.GetGenreById;

internal sealed class GetGenreByIdQueryHandler(IGenreRepository repository) : IQueryHandler<GetGenreByIdQuery, GenreDto>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genreId = new GenreId(request.Id);
        var genre = await _repository.GetByIdAsync(genreId, cancellationToken);
        var genreDto = genre.Adapt<GenreDto>();
        return genreDto;
    }
}
