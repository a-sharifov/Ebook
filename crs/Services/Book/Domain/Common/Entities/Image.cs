using Domain.Common.Ids;

namespace Domain.Common.Entities;

public class Image : Entity<ImageId>
{
    public ImageUrl ImageUrl { get; private set; }

    private Image(ImageId imageId, ImageUrl imageUrl)
    {
        Id = imageId;
        ImageUrl = imageUrl;
    }

    public static Result<Image> Create(ImageId imageId , ImageUrl imageUrl)
    {
        var image = new Image(imageId, imageUrl);

        // Add domain rules here

        return Result.Success(image);
    }
}