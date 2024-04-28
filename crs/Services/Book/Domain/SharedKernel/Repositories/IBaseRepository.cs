using Contracts.Paginations;
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
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    //Task<PagedList<TEntity>> GetPagedAsync(int skip, int take, Expression<Func<TEntity, object>>[]? includes = default, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(TStrongestId id, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(TStrongestId id, CancellationToken cancellationToken = default);
    Task DeleteAsync(TStrongestId id, CancellationToken cancellationToken = default);
    int Count(Expression<Func<TEntity, bool>>[]? wheres = default);
}
