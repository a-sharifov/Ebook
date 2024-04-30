using Domain.AuthorAggregate.Errors;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Authors.Commands.DeleteAuthor;

internal sealed class DeleteAuthorCommandHandler(
    IAuthorRepository repository, 
    IUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.AuthorId);
        var isExist = await _repository.IsExistAsync(authorId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                AuthorErrors.NotFound);
        }

        await _repository.DeleteAsync(authorId, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
