namespace Domain.BookAggregate.Ids;

public sealed record BookId(Guid Value) : IStrongestId;
