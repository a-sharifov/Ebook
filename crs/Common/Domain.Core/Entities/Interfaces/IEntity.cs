namespace Domain.Core.Entities.Interfaces;

/// <summary>
/// The base interface for an entity.
/// </summary>
/// <typeparam name="TId"> The type of the id.</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets the id of the entity.
    /// </summary>
    TId Id { get; }
}