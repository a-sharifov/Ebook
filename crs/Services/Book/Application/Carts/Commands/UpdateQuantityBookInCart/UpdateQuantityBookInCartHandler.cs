using Application.Carts.Commands.DeleteItemInCart;
using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Errors;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.CartAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using MediatR;

namespace Application.Carts.Commands.UpdateQuantityBookInCart;

internal sealed class UpdateQuantityBookInCartHandler(
    IUnitOfWork unitOfWork,
    ICartItemRepository cartItemRepository,
    IBookRepository bookRepository,
    ISender sender)
    : ICommandHandler<UpdateQuantityBookInCart>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(UpdateQuantityBookInCart request, CancellationToken cancellationToken)
    {
        var itemId = new CartItemId(request.CartItemId);
        var isExist = await _cartItemRepository.IsExistAsync(itemId, cancellationToken);

        if (!isExist)
        {
            Result.Failure(
                CartItemErrors.NotFound);
        }

        if (request.Quantity == 0)
        {
            var command = new DeleteItemInCartCommand(request.CartItemId);
            var result = await _sender.Send(command, cancellationToken);
            return result;
        }

        var item = await _cartItemRepository.GetAsync(itemId, cancellationToken);

        var quantity = CartItemQuantity.Create(request.Quantity).Value;

        var updateQuantityResult = item.UpdateQuantity(quantity);

        if (updateQuantityResult.IsFailure)
        {
            return Result.Failure(
                updateQuantityResult.Error);
        }

        await _cartItemRepository.UpdateAsync(item, cancellationToken);
        await _bookRepository.UpdateAsync(item.Book, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
