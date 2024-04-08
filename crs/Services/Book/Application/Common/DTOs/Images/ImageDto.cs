namespace Application.Common.DTOs.Images;

public sealed record ImageDto(
    Guid Id,
    string BucketName,
    string ImageName,
    string ImageType
    )
{
    public string GetPath() => $"{BucketName}/{ImageName}{ImageType}";
};
