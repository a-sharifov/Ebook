using Application.Common.DTOs.Genres;

namespace Application.Genres.Queries.GetAllGenres;

public sealed record GetAllGenresQuery : IQuery<IEnumerable<GenreDto>>;
