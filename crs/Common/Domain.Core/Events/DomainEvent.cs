using Domain.Core.Events.Interfaces;

namespace Domain.Core.Events;

/// <summary>
/// The abstract class for domain event.
/// </summary>
/// <param name="Id"> The id of the event.</param>
public abstract record DomainEvent(Guid Id) : IDomainEvent;