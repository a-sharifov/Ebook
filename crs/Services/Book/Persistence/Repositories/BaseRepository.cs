using Catalog.Persistence.Caching.Abstractions;
using Domain.SharedKernel.Repositories;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;
using Persistence.DbContexts;
using Infrasctrurcture.Core.Extensions;
using System.Linq.Expressions;
using Contracts.Paginations;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TStrongestId>(
    BookDbContext dbContext,
    ICachedEntityService<TEntity, TStrongestId> cached,
    TimeSpan expirationTime,
    bool enableBaseCaching = true)
    : IBaseRepository<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : class, IStrongestId
{
    protected readonly string _entityName = typeof(TEntity).Name;
    protected readonly BookDbContext _dbContext = dbContext;
    protected readonly ICachedEntityService<TEntity, TStrongestId> _cached = cached;
    protected readonly TimeSpan _cachingBaseExpirationTime = expirationTime;
    protected readonly bool _enableBaseCaching = enableBaseCaching;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AddAsync(entity, cancellationToken);

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (_enableBaseCaching)
        {
            await _cached.DeleteAsync(entity.Id, cancellationToken);
        }

        _dbContext.Remove(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);

        if (_enableBaseCaching)
        {
            await _cached.SetAsync(
                entity, cancellationToken: cancellationToken);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
         await GetEntityDbSet()
        .ToListAsync(cancellationToken: cancellationToken);

    protected async Task<PagedList<TEntity>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>[]? wheres = default,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        var skip = GetSkip(pageNumber, pageSize);

        var entities = await GetEntityDbSet()
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Wheres(wheres)
            .Includes(includes)
            .ToListAsync(cancellationToken);

        var count = Count(wheres);

        var pagedList = new PagedList<TEntity>(entities, count, pageNumber, pageSize);
        return pagedList;
    }

    public int Count(Expression<Func<TEntity, bool>>[]? wheres = default) =>
        GetEntityDbSet().Wheres(wheres).Count();

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().CountAsync(cancellationToken);

    public async Task<TEntity> GetAsync(
        TStrongestId id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _cached.GetAsync(id, cancellationToken);

        if (_enableBaseCaching)
        {
            if (entity is not null)
            {
                _dbContext.Attach(entity);
                return entity;
            }
        }

        entity = await GetEntityDbSet()
            .FirstAsync(c => c.Id == id, cancellationToken);

        if (_enableBaseCaching)
        {
            await _cached.SetAsync(
                entity, _cachingBaseExpirationTime, cancellationToken);
        }

        return entity;
    }

    public async Task<bool> IsExistAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);

    public async Task DeleteAsync(
        TStrongestId id,
        CancellationToken cancellationToken = default)
    {
        if (_enableBaseCaching)
        {
            await _cached.DeleteAsync(id, cancellationToken);
        }

        await GetEntityDbSet()
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    protected DbSet<TEntity> GetEntityDbSet() =>
        _dbContext.Set<TEntity>();

    protected int GetSkip(int pageNumber, int pageSize) =>
        (pageNumber - 1) * pageSize;
}
