using Domain.Core.UnitOfWorks.Interfaces;
using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Errors;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Domain.LanguageAggregate.ValueObjects;
using Persistence;

namespace Application.Languages.Commands.UpdateLanguage;

internal sealed class UpdateLanguageCommandHandler(
    ILanguageRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateLanguageCommand>
{
    private readonly ILanguageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var id = new LanguageId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                LanguageErrors.IsNotExist);
        }

        var name = LanguageName.Create(request.Name).Value;
        var code = LanguageCode.Create(request.Code).Value;

        var language = Language.Create(id, name, code).Value;

        await _repository.UpdateAsync(language, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}