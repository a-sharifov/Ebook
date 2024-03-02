namespace Domain.Core.StrongestIds;

/// <summary>
/// The base interface identifier that is the strongest in the domain.
/// </summary>
public interface IStrongestId
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    Guid Value { get; }
}
