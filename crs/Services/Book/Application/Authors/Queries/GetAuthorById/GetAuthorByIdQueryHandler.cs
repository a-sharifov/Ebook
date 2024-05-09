using Application.Common.DTOs.Authors;
using Domain.AuthorAggregate.Errors;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;

namespace Application.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorByIdQueryHandler(
    IAuthorRepository repository) 
    : IQueryHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IAuthorRepository _repository = repository;

    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new AuthorId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<AuthorDto>(
                AuthorErrors.NotFound);
        }

        var author = await _repository.GetAsync(id, cancellationToken);
        var authorDto = author.Adapt<AuthorDto>();

        return authorDto;
    }
}
