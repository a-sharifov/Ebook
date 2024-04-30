using Domain.Core.UnitOfWorks.Interfaces;
using Domain.LanguageAggregate.Errors;
using Domain.LanguageAggregate.Ids;
using Domain.LanguageAggregate.Repositories;

namespace Application.Languages.Commands.DeleteLanguage;

public sealed record DeleteLanguageCommand(Guid Id) : ICommand;

internal sealed class DeleteLanguageCommandHandler(
    IUnitOfWork unitOfWork, 
    ILanguageRepository repository) 
    : ICommandHandler<DeleteLanguageCommand>
{
    private readonly ILanguageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var id = new LanguageId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                LanguageErrors.IsNotExist);
        }

        await _repository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}