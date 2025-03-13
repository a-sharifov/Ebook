using Domain.Core.StrongestIds;

namespace Domain.OrderAggregate.Ids;

public sealed record OrderItemId(Guid Value) : IStrongestId;