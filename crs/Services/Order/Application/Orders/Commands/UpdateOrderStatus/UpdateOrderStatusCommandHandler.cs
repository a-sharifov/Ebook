using Domain.Core.UnitOfWorks.Interfaces;
using Domain.OrderAggregate.Errors;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.Repositories;
using EventBus.Common.Abstractions;
using Common.Contracts.Services.Orders.Events;
using EventBus.MassTransit.Abstractions;

namespace Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork,
    IMessageBus messageBus) : ICommandHandler<UpdateOrderStatusCommand, Order>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageBus _messageBus = messageBus;

    public async Task<Result<Order>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        
        if (!await _orderRepository.IsExistAsync(orderId, cancellationToken))
        {
            return Result.Failure<Order>(OrderErrors.OrderNotFound);
        }
        
        var order = await _orderRepository.GetAsync(orderId, cancellationToken);
        var oldStatus = order.Status;

        var updateResult = order.UpdateStatus(request.NewStatus);
        if (updateResult.IsFailure)
        {
            return Result.Failure<Order>(updateResult.Error);
        }

        _orderRepository.Update(order);
        await _unitOfWork.Commit(cancellationToken);

        var integrationEvent = new OrderStatusChangedIntegrationEvent(
            Guid.NewGuid(),
            order.Id.Value.ToString(),
            order.UserId,
            oldStatus.ToString(),
            order.Status.ToString(),
            "user@example.com" // TODO: Change logic
        );

        await _messageBus.Publish(integrationEvent, cancellationToken);

        return Result.Success(order);
    }
}
