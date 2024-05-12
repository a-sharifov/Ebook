using Domain.Core.UnitOfWorks.Interfaces;
using Domain.LanguageAggregate.Errors;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Domain.LanguageAggregate.ValueObjects;

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

        var language = await _repository.GetAsync(id, cancellationToken);

        var nameResult = LanguageName.Create(request.Name);
        var codeResult = LanguageCode.Create(request.Code);

        var firstFailureOrSuccessResult = 
            Result.FirstFailureOrSuccess(nameResult, codeResult);

        if (firstFailureOrSuccessResult.IsFailure)
        {
            return firstFailureOrSuccessResult;
        }

        language.Update(nameResult.Value, codeResult.Value);

        await _repository.UpdateAsync(language, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}