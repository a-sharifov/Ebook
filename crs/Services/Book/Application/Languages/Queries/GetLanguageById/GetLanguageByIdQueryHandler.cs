using Application.Common.DTOs.Languages;
using Domain.LanguageAggregate.Errors;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;

namespace Application.Languages.Queries.GetLanguageById;

internal sealed class GetLanguageByIdQueryHandler(
    ILanguageRepository repository) 
    : IQueryHandler<GetLanguageByIdQuery, LanguageDto>
{
    private readonly ILanguageRepository _repository = repository;

    public async Task<Result<LanguageDto>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new LanguageId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<LanguageDto>(
                LanguageErrors.IsNotExist);
        }

        var language = await _repository.GetAsync(id, cancellationToken);
        var languageDto = language.Adapt<LanguageDto>();

        return languageDto;
    }
}
