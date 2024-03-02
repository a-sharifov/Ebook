namespace Domain.Core.Events.Interfaces;

/// <summary>
/// The base interface for a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets the id of the event.
    /// </summary>
    Guid Id { get; }
}