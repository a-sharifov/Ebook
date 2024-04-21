using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid Id) : ICommand;

internal sealed class DeleteBookCommandHandler(
    IBookRepository repository, 
    IUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var id = new BookId(request.Id);
        var isExists = await _repository.IsExistsAsync(id, cancellationToken);

        if (!isExists)
        {
            return Result.Failure(
                BookErrors.BookIsNotExists);
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
