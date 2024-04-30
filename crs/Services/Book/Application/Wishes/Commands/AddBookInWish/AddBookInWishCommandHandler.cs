using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Ids;
using Domain.WishAggregate.Entities;
using Domain.WishAggregate.Errors;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate.Repositories;
using Persistence;

namespace Application.Wishes.Commands.AddBookInWish;

internal sealed class AddBookInWishCommandHandler(
    IWishRepository wishRepository,
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddBookInWishCommand>
{
    private readonly IWishRepository _wishRepository = wishRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddBookInWishCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.BookId);
        var bookIsExist = await _bookRepository.IsExistAsync(bookId, cancellationToken);

        if (!bookIsExist)
        {
            return Result.Failure(
                BookErrors.BookIsNotExist);
        }

        var book = await _bookRepository.GetAsync(bookId, cancellationToken);

        var userId = new UserId(request.UserId);
        var wish = await _wishRepository.GetAsync(userId, cancellationToken);

        var wishItemId = new WishItemId(Guid.NewGuid());
        var wishItem = WishItem.Create(wishItemId, book, wish.Id).Value;

        var addResult = wish.AddItem(wishItem);

        if (addResult.IsFailure)
        {
            return Result.Failure(
                addResult.Error);
        }

        await _wishRepository.UpdateAsync(wish, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}