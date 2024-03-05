namespace Domain.AuthorAggregate.Ids;

public record AuthorId(Guid Value) : IStrongestId;
