namespace Domain.Core.Events.Interfaces;

/// <summary>
/// The base interface for an entity that has domain events.
/// If an entity has domain events, it should implement this interface.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}