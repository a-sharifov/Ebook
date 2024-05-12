namespace Domain.Core.Messages.OutboxMessages;

/// <summary>
/// Record for outbox message consumer.
/// </summary>
/// <param name="Id"> The id of outbox message</param>
/// <param name="Name"> The name of outbox message</param>
public sealed record OutboxMessageConsumer(Guid Id, string Name);