using Domain.AuthorAggregate.Errors;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler(
    IAuthorRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var id = new AuthorId(request.Id);
        var isExist = await _repository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                AuthorErrors.NotFound);
        }

        var author = await _repository.GetAsync(id, cancellationToken);

        await _repository.UpdateAsync(author, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
