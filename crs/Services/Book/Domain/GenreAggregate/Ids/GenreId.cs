namespace Domain.GenreAggregate.Ids;

public sealed record GenreId(Guid Value) : IStrongestId;