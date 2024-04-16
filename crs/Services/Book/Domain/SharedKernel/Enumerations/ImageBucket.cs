using Domain.Core.Enumerations;

namespace Domain.SharedKernel.Enumerations;

public class ImageBucket(int value, string name) : Enumeration<ImageBucket>(value, name)
{
    public static ImageBucket Genres = new(0, "genres");
    public static ImageBucket Products = new(1, "products");
    public static ImageBucket Authots = new(2, "authors");
}
