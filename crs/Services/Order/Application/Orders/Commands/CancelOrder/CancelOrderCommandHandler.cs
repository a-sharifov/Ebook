using Domain.Core.UnitOfWorks.Interfaces;
using Domain.OrderAggregate.Errors;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.Repositories;

namespace Application.Orders.Commands.CancelOrder;

internal sealed class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        
        if (!await _orderRepository.IsExistAsync(orderId, cancellationToken))
        {
            return Result.Failure(OrderErrors.OrderNotFound);
        }
        
        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        var cancelResult = order.Cancel();
        if (cancelResult.IsFailure)
        {
            return Result.Failure(cancelResult.Error);
        }

        _orderRepository.Update(order);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
