namespace Domain.SharedKernel.Ids;

public sealed record ImageId(Guid Value) : IStrongestId;
