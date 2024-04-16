namespace Domain.CartAggregate.Ids;

public sealed record CartId(Guid Value) : IStrongestId;
