using Application.Common.DTOs.Genres;

namespace Application.Genres.Queries.GetGenres;

public sealed record GetGenresQuery() : IQuery<IEnumerable<GenreDto>>;
