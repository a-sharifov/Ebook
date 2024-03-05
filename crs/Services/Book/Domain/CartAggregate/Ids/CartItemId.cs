namespace Domain.CartAggregate.Ids;

public sealed record CartItemId(Guid Value) : IStrongestId;
