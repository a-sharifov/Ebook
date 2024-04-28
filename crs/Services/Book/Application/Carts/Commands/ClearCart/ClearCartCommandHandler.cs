using Domain.CartAggregate.Repositories;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Ids;

namespace Application.Carts.Commands.ClearCart;

internal sealed class ClearCartCommandHandler(
    ICartRepository repository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<ClearCartCommand>
{
    private readonly ICartRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var id = new UserId(request.Id);
        var cart = await _repository.GetAsync(id, cancellationToken);

        cart.Clear();

        await _repository.UpdateAsync(cart, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
