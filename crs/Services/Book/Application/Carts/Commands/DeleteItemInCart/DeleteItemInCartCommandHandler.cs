using Domain.BookAggregate.Repositories;
using Domain.BookAggregate.ValueObjects;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Carts.Commands.DeleteItemInCart;

internal sealed class DeleteItemInCartCommandHandler(
    ICartItemRepository cartItemRepository,
    IUnitOfWork unitOfWork,
    IBookRepository bookRepository)
    : ICommandHandler<DeleteItemInCartCommand>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteItemInCartCommand request, CancellationToken cancellationToken)
    {
        var itemId = new CartItemId(request.ItemId);
        var isExist = await _cartItemRepository.IsExistAsync(itemId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                CartErrors.ItemNotFound);
        }

        var item = await _cartItemRepository.GetAsync(itemId, cancellationToken);
        var book = await _bookRepository.GetAsync(item.Book.Id, cancellationToken);

        var bookQuantity = QuantityBook.Create(item.Quantity.Value).Value;
        book.AddQuantity(bookQuantity);

        await _cartItemRepository.DeleteAsync(itemId, cancellationToken);
        await _bookRepository.UpdateAsync(book, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}