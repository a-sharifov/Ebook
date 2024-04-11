using Domain.SharedKernel.Ids;

namespace Domain.SharedKernel.Entities;

public class Image : Entity<ImageId>
{
    public BucketName BucketName { get; private set; }
    public ImageName Name { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Image() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Image(ImageId id, BucketName bucketName, ImageName name) =>
        (Id, BucketName, Name) = (id, bucketName, name);

    public string Path => $"/{BucketName}/{Name}";

    public string UrlString(string domainName) => domainName + Path;

    public static Result<Image> Create(ImageId id, BucketName bucketName, ImageName name)
    {
        var image = new Image(id, bucketName, name);

        // todo: add domain event

        return image;
    }
}