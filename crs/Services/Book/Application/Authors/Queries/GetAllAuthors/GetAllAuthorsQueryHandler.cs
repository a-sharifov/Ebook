using Application.Common.DTOs.Authors;
using Domain.AuthorAggregate.Repositories;

namespace Application.Authors.Queries.GetAllAuthors;

internal sealed class GetAllAuthorsQueryHandler(
    IAuthorRepository repository) 
    : IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAuthorRepository _repository = repository;

    public async Task<Result<IEnumerable<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _repository.GetAllAsync(cancellationToken);

        var authorsDto = authors.Adapt<IEnumerable<AuthorDto>>();

        return Result.Success(authorsDto);
    }
}
