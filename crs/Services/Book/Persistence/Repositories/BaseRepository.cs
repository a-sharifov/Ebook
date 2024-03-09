using Catalog.Persistence.Caching.Abstractions;
using Domain.SharedKernel.Repositores;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TStrongestId>
    : IBaseRepository<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : class, IStrongestId
{
    protected readonly string _entityName;
    protected readonly BookDbContext _dbContext;
    protected readonly ICachedEntityService<TEntity, TStrongestId> _cached;
    protected readonly TimeSpan _expirationTime;

    protected BaseRepository(
        BookDbContext dbContext,
        ICachedEntityService<TEntity, TStrongestId> cached,
        TimeSpan expirationTime)
    {
        _entityName = typeof(TEntity).Name;
        _dbContext = dbContext;
        _cached = cached;
        _expirationTime = expirationTime;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await _dbContext
        .Set<TEntity>()
        .AddAsync(entity, cancellationToken);

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _cached.DeleteAsync(entity.Id, cancellationToken);
        _dbContext.Remove(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);
        await _cached.RefreshAsync(entity.Id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
         await _dbContext.Set<TEntity>().ToListAsync(cancellationToken: cancellationToken);

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        await _cached
            .SetAsync(entities, _expirationTime, cancellationToken);

        return entities;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>().CountAsync(cancellationToken);

    public async Task<TEntity?> GetByIdAsync(TStrongestId id, CancellationToken cancellationToken = default)
    {
        var entity = await _cached.GetAsync(id, cancellationToken);

        if (entity is not null)
        {
            return entity;
        }

        entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (entity is null)
        {
            return entity;
        }

        await _cached.SetAsync(entity, _expirationTime, cancellationToken);
        return entity;
    }

    public async Task<bool> IsExistsAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await GetByIdAsync(id, cancellationToken) is not null;

    public async Task DeleteByIdAsync(TStrongestId id, CancellationToken cancellationToken = default)
    {
        await _cached.DeleteAsync(id, cancellationToken);

        await _dbContext
            .Set<TEntity>()
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TStrongestId> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        await _cached.SetAsync(entities, cancellationToken: cancellationToken);

        return entities;
    }
}
