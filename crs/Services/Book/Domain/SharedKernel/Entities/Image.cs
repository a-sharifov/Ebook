using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;

namespace Domain.SharedKernel.Entities;

public class Image : Entity<ImageId>
{
    public string BucketName { get; private set; }
    public string ImageName { get; private set; }
    public ImageType ImageType { get; private set; }

    private Image(ImageId id, string bucketName, string imageName, ImageType imageType) =>
        (Id, BucketName, ImageName, ImageType) = (id, bucketName, imageName, imageType);

    public string FullName => $"{ImageName}{ImageType.Name}";

    public string Path => $"/{BucketName}/{FullName}";

    public string UrlString(string domainName) => domainName + Path;


    public static Result<Image> Create(ImageId id, string bucketName, ImageType imageType)
    {
        var isBucketNameNullOrEmpty = bucketName.IsNullOrEmpty();

        if (isBucketNameNullOrEmpty)
        {
            return Result.Failure<Image>(
                ImageErrors.ImageNameCannotBeEmpty);
        }

        var imageName = Guid.NewGuid().ToString();

        var image = new Image(id, bucketName, imageName, imageType);
        return image;
    }

}