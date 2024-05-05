using Domain.AuthorAggregate;
using Domain.AuthorAggregate.Errors;
using Domain.AuthorAggregate.Ids;
using Domain.AuthorAggregate.Repositories;
using Domain.AuthorAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Authors.Commands.AddAuthor;

internal sealed class AddAuthorCommandHandler(
    IAuthorRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddAuthorCommand>
{
    private readonly IAuthorRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var pseudonym = Pseudonym.Create(request.Pseudonym).Value;
        var isExist = await _repository.IsExistAsync(pseudonym, cancellationToken);

        if (isExist)
        {
            return Result.Failure(
                AuthorErrors.IsExist);
        }   

        var authorId = new AuthorId(Guid.NewGuid());
        var author = Author.Create(authorId, pseudonym).Value;

        await _repository.AddAsync(author, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
