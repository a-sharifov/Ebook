using System.Linq.Expressions;

namespace Domain.SharedKernel.Repositories;

public interface IBaseRepository<TEntity, TStrongestId>
    : IRepository<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : IStrongestId
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>? includes = default, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(TStrongestId id, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(TStrongestId id, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(TStrongestId id, CancellationToken cancellationToken = default);
}
