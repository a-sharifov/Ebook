using Contracts.Paginations;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;
using Domain.OrderAggregate.Errors;
using Domain.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TStrongestId> : IBaseRepository<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : class, IStrongestId
{
    protected readonly string _entityName = typeof(TEntity).Name;
    protected readonly OrderDbContext _dbContext;

    protected BaseRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AddAsync(entity, cancellationToken);

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);
        await Task.CompletedTask;
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().ToListAsync(cancellationToken: cancellationToken);

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().CountAsync(cancellationToken);

    public async Task<TEntity> GetAsync(TStrongestId id, CancellationToken cancellationToken = default)
    {
        if (!await IsExistAsync(id, cancellationToken))
        {
            throw new InvalidOperationException($"Entity {_entityName} with ID {id} not found");
        }

        var entity = await GetEntityDbSet()
            .FirstAsync(c => c.Id.Equals(id), cancellationToken);

        return entity;
    }

    public async Task<bool> IsExistAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AnyAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);

    public async Task DeleteAsync(TStrongestId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        await DeleteAsync(entity, cancellationToken);
    }

    public int Count(Expression<Func<TEntity, bool>>[]? wheres = default)
    {
        var query = GetEntityDbSet().AsQueryable();
        
        if (wheres != null)
        {
            foreach (var where in wheres)
            {
                query = query.Where(where);
            }
        }
        
        return query.Count();
    }

    protected DbSet<TEntity> GetEntityDbSet() =>
        _dbContext.Set<TEntity>();
}
