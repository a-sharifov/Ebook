using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Carts.Commands.DeleteBookInCart;

internal sealed class DeleteItemInCartCommandHandler(
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork,
    IBookRepository bookRepository)
    : ICommandHandler<DeleteItemInCartCommand>
{
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteItemInCartCommand request, CancellationToken cancellationToken)
    {
        var itemId = new CartItemId(request.ItemId);
        var isExist = await _cartRepository.IsExistAsync(itemId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                CartErrors.ItemNotFound);
        }

        var item = await _cartRepository.GetAsync(itemId, cancellationToken);
        var book = await _bookRepository.GetAsync(item.Book.Id, cancellationToken);

        await _cartRepository.DeleteAsync(itemId, cancellationToken);
        await _bookRepository.UpdateAsync(book, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}