using Domain.Core.UnitOfWorks.Interfaces;
using Domain.LanguageAggregate;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;
using Domain.LanguageAggregate.ValueObjects;

namespace Application.Languages.Commands.AddLanguage;

internal sealed class AddLanquageCommandHandler(
    ILanguageRepository repository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<AddLanquageCommand>
{
    private readonly ILanguageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddLanquageCommand request, CancellationToken cancellationToken)
    {
        var id = new LanguageId(Guid.NewGuid());
        var nameResult = LanguageName.Create(request.Name);
        var codeResult = LanguageCode.Create(request.Code);

        var firstFailureOrSuccessResult = Result.FirstFailureOrSuccess(nameResult, codeResult);

        if (firstFailureOrSuccessResult.IsFailure)
        {
            return firstFailureOrSuccessResult;
        }

        var language = Language.Create(id, nameResult.Value, codeResult.Value).Value;

        await _repository.AddAsync(language, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}

