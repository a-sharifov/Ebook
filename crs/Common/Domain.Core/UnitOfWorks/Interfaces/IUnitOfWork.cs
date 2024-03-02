namespace Domain.Core.UnitOfWorks.Interfaces;

/// <summary>
/// The interface for unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits the changes.
    /// </summary>
    Task<int> Commit(CancellationToken cancellationToken = default);
}
