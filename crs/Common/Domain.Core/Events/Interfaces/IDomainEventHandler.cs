namespace Domain.Core.Events.Interfaces;

/// <summary>
/// The base interface domain event handler interface.
/// </summary>
/// <typeparam name="TDomainEvent"> The domain event type.</typeparam>
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
where TDomainEvent : IDomainEvent
{
}