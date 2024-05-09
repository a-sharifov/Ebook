using Application.Common.DTOs.Languages;
using Domain.LanguageAggregate.Repositories;

namespace Application.Languages.Queries.GetAllLanguages;

internal sealed class GetAllLanguagesQueryHandler(
    ILanguageRepository repository) 
    : IQueryHandler<GetAllLanguagesQuery, IEnumerable<LanguageDto>>
{
    private readonly ILanguageRepository _repository = repository;

    public async Task<Result<IEnumerable<LanguageDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _repository.GetAllAsync(cancellationToken: cancellationToken);

        var languagesDto = languages.Adapt<IEnumerable<LanguageDto>>();
        return Result.Success(languagesDto);
    }
}