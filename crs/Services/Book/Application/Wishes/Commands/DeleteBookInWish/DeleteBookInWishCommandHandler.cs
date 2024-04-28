using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Repositories;

namespace Application.Wishes.Commands.DeleteBookInWish;

internal sealed class DeleteBookInWishCommandHandler(
    IWishRepository wishRepository,
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteBookInWishCommand>
{
    private readonly IWishRepository _wishRepository = wishRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBookInWishCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.BookId);
        var bookIsExist = await _bookRepository.IsExistAsync(bookId, cancellationToken);

        if (!bookIsExist)
        {
            return Result.Failure(
                BookErrors.BookIsNotExist);
        }

        var userId = new UserId(request.UserId);
        var wish = await _wishRepository.GetAsync(userId, cancellationToken);

        var removeItemResult = wish.RemoveItem(bookId);

        if (removeItemResult.IsFailure)
        {
            return Result.Failure(
                removeItemResult.Error);
        }

        await _wishRepository.UpdateAsync(wish, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
