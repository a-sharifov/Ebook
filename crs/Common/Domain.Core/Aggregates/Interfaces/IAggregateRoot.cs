using Domain.Core.Entities.Interfaces;

namespace Domain.Core.Aggregates.Interfaces;

/// <summary>
/// The base interface for an aggregate root.
/// </summary>
/// <typeparam name="TId"> The type of the id.</typeparam>
public interface IAggregateRoot<TId> : IEntity<TId>
{
}
