using Domain.Core.Aggregates.Interfaces;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;

namespace Domain.Core.Aggregates;

/// <summary>
/// The base class for an aggregate root.
/// </summary>
/// <typeparam name="TStrongestId"> The strongest id type.</typeparam>
public abstract class AggregateRoot<TStrongestId> : Entity<TStrongestId>,
    IAggregateRoot<TStrongestId>
    where TStrongestId : IStrongestId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{StrongestId}"/> class.
    /// </summary>
    protected AggregateRoot() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{StrongestId}"/> class.
    /// </summary>
    /// <param name="id"> The id.</param>
    protected AggregateRoot(TStrongestId id) : base(id) { }
}