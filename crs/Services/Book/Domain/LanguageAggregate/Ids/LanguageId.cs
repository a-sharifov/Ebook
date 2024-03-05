namespace Domain.LanguageAggregate.Ids;

public sealed record LanguageId(Guid Value) : IStrongestId;
