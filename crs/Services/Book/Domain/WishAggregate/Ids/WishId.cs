namespace Domain.WishAggregate.Ids;

public sealed record WishId(Guid Value) : IStrongestId;
