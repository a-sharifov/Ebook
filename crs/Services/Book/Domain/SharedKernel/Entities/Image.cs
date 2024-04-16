using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;

namespace Domain.SharedKernel.Entities;

public class Image : Entity<ImageId>
{
    public ImageBucket Bucket { get; private set; }
    public ImageName Name { get; private set; }
    public ImageUrl Url { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Image() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Image(ImageId id, ImageBucket bucket, ImageName name, ImageUrl url)
    {
        Id = id;
        Bucket = bucket;
        Name = name;
        Url = url;
    }

    public static Result<Image> Create(ImageId id, ImageBucket bucket, ImageName name, ImageUrl url)
    {
        var image = new Image(id, bucket, name, url);

        // todo: add domain event

        return image;
    }
}