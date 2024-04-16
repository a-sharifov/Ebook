using Catalog.Persistence.Caching.Abstractions;
using Domain.Core.Entities;
using Domain.Core.StrongestIds;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.Persistence.Caching;

public sealed class CachedEntityService<TEntity, TStrongestId>(IDistributedCache cache) 
    : CachedService(cache), 
    ICachedEntityService<TEntity, TStrongestId>
    where TEntity : Entity<TStrongestId>
    where TStrongestId : class, IStrongestId
{
    private readonly string _entityName = typeof(TEntity).Name;

    private string GetKey(TStrongestId id) =>
    $"{_entityName}-{id.Value}";

    private string GetKey(TEntity entity) =>
     GetKey(entity.Id);

    public async Task<TEntity?> GetAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await GetAsync<TEntity>(GetKey(id), cancellationToken);

    public async Task SetAsync(
        TEntity entity,
        TimeSpan expirationDate = default,
        CancellationToken cancellationToken = default) =>
        await SetAsync(
            GetKey(entity),
            entity,
            expirationDate,
            cancellationToken);

    public async Task RefreshAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await RefreshAsync(GetKey(id), cancellationToken);

    public async Task DeleteAsync(TStrongestId id, CancellationToken cancellationToken = default) =>
        await DeleteAsync(GetKey(id), cancellationToken);

    public async Task SetAsync(
        IEnumerable<TEntity> entities,
        TimeSpan expirationDate = default,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await SetAsync(entity, expirationDate, cancellationToken);
        }
    }
}
