﻿using Catalog.Persistence.Caching.Abstractions;
using Domain.SharedKernel.Repositories;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;
using Persistence.DbContexts;
using Infrasctrurcture.Core.Extensions;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TStrongestId>(
    BookDbContext dbContext,
    ICachedEntityService<TEntity, TStrongestId> cached,
    TimeSpan expirationTime)
    : IBaseRepository<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : class, IStrongestId
{
    protected readonly string _entityName = typeof(TEntity).Name;
    protected readonly BookDbContext _dbContext = dbContext;
    protected readonly ICachedEntityService<TEntity, TStrongestId> _cached = cached;
    protected readonly TimeSpan _expirationTime = expirationTime;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AddAsync(entity, cancellationToken);

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

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, object>>[]? includes = default, 
        CancellationToken cancellationToken = default) =>
         await GetEntityDbSet()
        .Includes(includes)
        .ToListAsync(cancellationToken: cancellationToken);

    public async Task<IEnumerable<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, object>>[]? includes, 
        int skip, 
        int take, 
        CancellationToken cancellationToken = default)
    {
        var entities = await GetEntityDbSet()
            .Skip(skip)
            .Take(take)
            .Includes(includes)
            .ToListAsync(cancellationToken);

        await _cached
            .SetAsync(entities, _expirationTime, cancellationToken);

        return entities;
    }

    public int Count(Expression<Func<TEntity, bool>>[] wheres) =>
        GetEntityDbSet().Wheres(wheres).Count();

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().CountAsync(cancellationToken);

    public async Task<TEntity> GetByIdAsync(
        TStrongestId id, 
        Expression<Func<TEntity, object>>[]? includes = default, 
        CancellationToken cancellationToken = default)
    {
        var entity = await _cached.GetAsync(id, cancellationToken);

        if (entity is not null)
        {
            return entity;
        }

        entity = await GetEntityDbSet()
            .Includes(includes)
            .FirstAsync(c => c.Id == id, cancellationToken);

        await _cached.SetAsync(entity, _expirationTime, cancellationToken);
        return entity;
    }

    public async Task<bool> IsExistsAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await GetByIdAsync(id, cancellationToken: cancellationToken) is not null;

    public async Task DeleteByIdAsync(
        TStrongestId id, 
        CancellationToken cancellationToken = default)
    {
        await _cached.DeleteAsync(id, cancellationToken);

        await GetEntityDbSet()
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(
        IEnumerable<TStrongestId> ids,
        Expression<Func<TEntity, object>>[]? includes = default,
        CancellationToken cancellationToken = default)
    {
        var entities = await GetEntityDbSet()
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .Includes(includes)
            .ToListAsync(cancellationToken);

        await _cached.SetAsync(entities, cancellationToken: cancellationToken);

        return entities;
    }

    public DbSet<TEntity> GetEntityDbSet() => 
        _dbContext.Set<TEntity>();
}
