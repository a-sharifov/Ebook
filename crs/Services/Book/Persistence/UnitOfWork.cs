using Contracts.Serializers;
using Domain.Core.Events.Interfaces;
using Domain.Core.Messages;
using Domain.Core.Messages.OutboxMessages;
using Domain.Core.UnitOfWorks.Interfaces;
using Persistence.DbContexts;
using System.Collections.Immutable;

namespace Persistence;

public sealed class UnitOfWork(BookDbContext bookDbContext) : IUnitOfWork
{
    private readonly BookDbContext _dbContext = bookDbContext;

    public async Task<int> Commit(CancellationToken cancellationToken = default)
    {
        var entityEntryChange = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>().ToImmutableList();

        var outboxMessages = entityEntryChange
            .Select(x => x.Entity)
            .SelectMany(hasDomainEvents =>
            {
                var domainEvents = hasDomainEvents.DomainEvents;
                hasDomainEvents.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                id: Guid.NewGuid(),
                createdAt: DateTime.UtcNow,
                type: domainEvent.GetType().Name,
                message: JsonSerializer.SerializeObject(domainEvent))
            );


        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
