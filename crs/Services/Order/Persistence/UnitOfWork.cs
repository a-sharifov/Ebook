using Contracts.Serializers;
using Domain.Core.Events.Interfaces;
using Domain.Core.Messages.OutboxMessages;
using Domain.Core.UnitOfWorks.Interfaces;
using Persistence.DbContexts;

namespace Persistence;

public sealed class UnitOfWork(OrderDbContext dbContext) : IUnitOfWork
{
    private readonly OrderDbContext _dbContext = dbContext;

    public async Task<int> Commit(CancellationToken cancellationToken = default)
    {
        await SendDomainEventsToOutboxMessagesAsync(cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SendDomainEventsToOutboxMessagesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntryChange = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>().ToList();

        var outboxMessages = entityEntryChange
            .Select(x => x.Entity)
            .SelectMany(hasDomainEvents =>
            {
                var domainEvents = hasDomainEvents.DomainEvents;
                //hasDomainEvents.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                id: Guid.NewGuid(),
                createdAt: DateTime.UtcNow,
                type: domainEvent.GetType().Name,
                message: JsonSerializer.SerializeObject(domainEvent))
            );

        await _dbContext
            .Set<OutboxMessage>()
            .AddRangeAsync(outboxMessages, cancellationToken);
    }
}
