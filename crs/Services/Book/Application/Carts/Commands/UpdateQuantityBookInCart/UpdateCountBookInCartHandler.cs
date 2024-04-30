using Domain.CartAggregate.Ids;
using Domain.CartAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;

namespace Application.Carts.Commands.UpdateCountBookInCart;

internal sealed class UpdateCountBookInCartHandler(
    IUnitOfWork unitOfWork, 
    ICartRepository cartRepository)
    : ICommandHandler<UpdateCountBookInCart>
{
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateCountBookInCart request, CancellationToken cancellationToken)
    {
        throw new Exception();
        //var itemId = new CartItemId(request.CartItemId);
        //var isExist = await _cartRepository.IsExistAsync(itemId, cancellationToken);

        //if (!isExist)
        //{

        //}

        //var item = await _cartRepository.GetAsync(itemId, cancellationToken);

        //item.AddQuantity();

        ////await _cartRepository.UpdateAsync(item, cancellationToken);
        //await _unitOfWork.Commit(cancellationToken);
    }
}
