namespace Application.Common.DTOs.Images;

public sealed record ImageDto(
    Guid Id,
    string BucketName,
    string Name)
{
    public string GetPath() => $"{BucketName}/{Name}";
};
