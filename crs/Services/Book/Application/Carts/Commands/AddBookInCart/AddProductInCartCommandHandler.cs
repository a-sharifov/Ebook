using Domain.BookAggregate.Errors;
using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.CartAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Commands.AddBookInCart;

internal sealed class AddProductInCartCommandHandler(
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork,
    IBookRepository bookRepository)
    : ICommandHandler<AddProductInCartCommand>
{
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.BookId);
        var isExist = await _bookRepository.IsExistAsync(bookId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                BookErrors.BookIsNotExist);
        }

        var userId = new UserId(request.UserId);
        var book = await _bookRepository.GetAsync(bookId, cancellationToken);
        var cart = await _cartRepository.GetAsync(userId, cancellationToken);

        var cartItemQuantity = CartItemQuantity.Default();
        var itemId = new CartItemId(Guid.NewGuid());

        var item = CartItem.Create(itemId, cart.Id, book, cartItemQuantity).Value;
        var addItemResult = cart.AddItem(item);

        if (addItemResult.IsFailure)
        {
            return Result.Failure(
                addItemResult.Error);
        }

        await _cartRepository.UpdateAsync(cart, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
} 