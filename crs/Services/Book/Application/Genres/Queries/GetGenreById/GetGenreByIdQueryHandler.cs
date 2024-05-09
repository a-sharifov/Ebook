﻿using Application.Common.DTOs.Genres;
using Domain.GenreAggregate.Errors;
using Domain.GenreAggregate.Ids;
using Domain.GenreAggregate.Repositories;

namespace Application.Genres.Queries.GetGenreById;

internal sealed class GetGenreByIdQueryHandler(
    IGenreRepository repository) 
    : IQueryHandler<GetGenreByIdQuery, GenreDto>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new GenreId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<GenreDto>(
                GenreErrors.IsNotExist);
        }

        var genre = await _repository.GetAsync(id, cancellationToken);

        var genreDto = genre.Adapt<GenreDto>();

        return genreDto;
    }
}
