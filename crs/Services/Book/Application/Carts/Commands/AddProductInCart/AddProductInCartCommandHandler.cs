using Domain.BookAggregate.Ids;
using Domain.BookAggregate.Repositories;
using Domain.CartAggregate.Entities;
using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.CartAggregate.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Commands.AddProductInCart;

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
        var cartId = new UserId(request.UserId);
        var cart = await _cartRepository.GetAsync(cartId, cancellationToken);

        var bookId = new BookId(request.BookId);
        var book = await _bookRepository.GetAsync(bookId, cancellationToken);

        var cartItemQuantity = CartItemQuantity.Create(request.Quantity).Value;
        var itemId = new CartItemId(Guid.NewGuid());
        var item = CartItem.Create(itemId, cart.Id, book, cartItemQuantity).Value;

        cart.AddItem(item);

        await _cartRepository.UpdateAsync(cart, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
} 