namespace Domain.UserAggregate.Ids;

public record UserId(Guid Value) : IStrongestId;
