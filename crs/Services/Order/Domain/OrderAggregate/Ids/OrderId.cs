using Domain.Core.StrongestIds;

namespace Domain.OrderAggregate.Ids;

public sealed record OrderId(Guid Value) : IStrongestId;
