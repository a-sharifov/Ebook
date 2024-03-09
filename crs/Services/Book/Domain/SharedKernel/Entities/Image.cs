using Domain.SharedKernel.Ids;

namespace Domain.SharedKernel.Entities;

public class Image : Entity<ImageId>
{
    public ImageUrl ImageUrl { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Image() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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